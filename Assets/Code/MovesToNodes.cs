using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class MovesToNodes : MonoBehaviour
{
	public float AttractionForce = 0.025f;
	private Rigidbody2D body;

	void Start ()
	{
		body = this.gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		moveToNodesWithTag ("Node");
	}

	private void moveToNodesWithTag (string tag)
	{
		/*
		List<GameObject> allNodes = new List<GameObject> (GameObject.FindGameObjectsWithTag (tag));

		foreach (GameObject node in allNodes) {
			float distanceToOtherNode = Vector3.Distance (node.transform.position, gameObject.transform.position);
			float forceMagnitude = 0;

			if (distanceToOtherNode > 0) {
				forceMagnitude = ((1 / distanceToOtherNode) + 1) * AttractionForce;
			}

			if (forceMagnitude > 0) {
				Vector3 toOtherNode = node.transform.position - gameObject.transform.position;
				Vector3 forceToOtherNode = toOtherNode * forceMagnitude;

				body.AddForce(forceToOtherNode);
			}
		}*/
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		foreach(ContactPoint2D contact in coll.contacts)
		{
			body.AddForce(contact.normal * 5.0f);
			
		}		
	}
}
