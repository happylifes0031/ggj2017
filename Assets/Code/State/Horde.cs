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

	void OnCollisionStay2D(Collision2D coll)
	{
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		foreach (ContactPoint2D contact in coll.contacts)
		{
			rigidBody.AddForce(contact.normal * 4.0f);
		}
	}
}

public class Horde : MonoBehaviour
{
	private GameObject baseNodePrefab;

	public List<GameObject> Nodes { get; private set; }

	void CreateNodePrefab()
	{
		baseNodePrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		baseNodePrefab.SetActive(false);
		baseNodePrefab.layer = 8;
		baseNodePrefab.AddComponent<HordeNode>();
		SphereCollider collider = baseNodePrefab.GetComponent<SphereCollider>();
		DestroyImmediate(collider);
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
	void Update()
	{
		// simulate nodes
		foreach( GameObject node in Nodes)
		{

		}
	}
}
