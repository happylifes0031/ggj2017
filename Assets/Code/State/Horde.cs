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
	
    void OnCollisionEnter2D(Collision2D coll)
	{
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		foreach (ContactPoint2D contact in coll.contacts)
		{
			if(coll.gameObject.tag == "Node")
				rigidBody.AddForce(contact.normal * 220.0f);
			if (coll.gameObject.tag == "Wave")
				rigidBody.AddForce(contact.normal * 2040.0f);
			if (coll.gameObject.tag == "Wolf")
			{
				if(Vector3.Distance(transform.position, 
					coll.gameObject.transform.position) < 10.0f)
				{
					GameState.gameState.horde.Nodes.Remove(this.gameObject);

					DestroyObject(this.gameObject);
				}

				rigidBody.AddForce(contact.normal * 3000f);

			}

		}
	}
}

public class Horde : MonoBehaviour
{
	private GameObject baseNodePrefab;

	public static Horde horde;
	public List<GameObject> Nodes { get; private set; }
	public GameObject Test;
	public float TotalTime;

	public Vector3 CenterOfHorde { get; private set; }
	private float startTime;

	void CreateNodePrefab()
	{
		baseNodePrefab = Instantiate(Test); ;
		baseNodePrefab.SetActive(false);
		baseNodePrefab.tag = "Node";
		baseNodePrefab.layer = 8;
		baseNodePrefab.AddComponent<HordeNode>();
		Rigidbody2D rigidBody = baseNodePrefab.AddComponent<Rigidbody2D>();
		rigidBody.gravityScale = 0.0f;
		CircleCollider2D sphereCollider = baseNodePrefab.AddComponent<CircleCollider2D>();
	}

	void Awake ()
	{
		horde = this;
		startTime = Time.time;
		TotalTime = 0f;
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
		TotalTime = Time.time - startTime;
		
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

			Vector3 direction = Vector3.Scale((Vector3.zero - node.transform.position).normalized, (new Vector3(1f, 1f, 0f)));

			rigidBody.AddForce(direction * 20.0f);
		}
	}
}
