using System.Collections.Generic;
using UnityEngine;

public class HordeNode : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{

	}

	//void OnCollisionStay2D(Collision2D coll)
    void OnCollisionEnter2D(Collision2D coll)
	{
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		foreach (ContactPoint2D contact in coll.contacts)
		{
			rigidBody.AddForce(contact.normal * 20.0f);
		}
	}
}

public class Horde : MonoBehaviour
{
	private GameObject baseNodePrefab;

	public List<GameObject> Nodes { get; private set; }

	public GameObject Test;

	public Vector3 CenterOfHorde { get; private set; }

	void CreateNodePrefab()
	{
		#region
		/* Might need for animation
		var model = (Avatar)Resources.Load("Models/NodeModel", typeof(Avatar));

		var controllers = Resources.LoadAll("Models/NodeModel", typeof(RuntimeAnimatorController));
		Animator animator = baseNodePrefab.AddComponent<Animator>();
		animator.avatar = model;
		//animator.runtimeAnimatorController = (RuntimeAnimatorController)controllers[0];

		Animation animation = baseNodePrefab.AddComponent<Animation>();
		//var clips = Resources.LoadAll("Models/NodeModel", typeof(AnimationClip));

		//animation.clip = (AnimationClip)clips[0];
		//animation.playAutomatically = true;
		//animation.enabled = true;
		*/
		#endregion

		baseNodePrefab = Instantiate(Test); ;
		baseNodePrefab.SetActive(false);
		baseNodePrefab.layer = 8;
		baseNodePrefab.AddComponent<HordeNode>();
		Rigidbody2D rigidBody = baseNodePrefab.AddComponent<Rigidbody2D>();
		rigidBody.gravityScale = 0.0f;
		CircleCollider2D sphereCollider = baseNodePrefab.AddComponent<CircleCollider2D>();
	}

	void Start ()
	{
		CreateNodePrefab();

		//
		Nodes = new List<GameObject>();

		// spawn in grid
		for(int x = 0; x < 10; x++)
		{
			for(int y = 0; y < 10; y++)
			{
				float xRand = Random.value - 0.5f;
				float yRand = Random.value - 0.5f;
				GameObject node = Object.Instantiate(baseNodePrefab, new Vector3(x + xRand, y + yRand, -0.1f), Quaternion.identity);
				node.SetActive(true);
				Nodes.Add(node);

			}
		}
	}

	// Update is called once per frame
	float randomCollisionCircle = 0.0f;
	void Update()
	{
		Vector3 centerOfHorde = Vector3.zero;
		
        foreach (GameObject node in Nodes)
		{
			centerOfHorde += node.transform.position;

			randomCollisionCircle += Random.value;
			CircleCollider2D circle = node.GetComponent<CircleCollider2D>();
			circle.radius = 0.005f + ((Mathf.Sin(randomCollisionCircle) + 1.0f) * 0.002f);

			float rX = (Random.value * 2.0f) - 1.0f;
			float rY = (Random.value * 2.0f) - 1.0f;

			Rigidbody2D rigidBody = node.GetComponent<Rigidbody2D>();
			rigidBody.AddForce(new Vector2(rX, rY) * 10.0f);
		}

		centerOfHorde = centerOfHorde / Nodes.Count;
		CenterOfHorde = centerOfHorde;
		CenterOfHorde.Scale(new Vector3(1f, 1f, 0f));

		foreach (GameObject node in Nodes)
		{
			Rigidbody2D rigidBody = node.GetComponent<Rigidbody2D>();

			Vector3 direction = Vector3.Scale((centerOfHorde - node.transform.position).normalized, (new Vector3(1f, 1f, 0f)));

			rigidBody.AddForce(direction * 10.0f);
		}
	}
}
