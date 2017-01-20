using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class MovesToNodes : MonoBehaviour
{
	public float attractionForce = 0.003f;
	private Rigidbody body;

	void Start ()
	{
		attractionForce = 0.003f;
		body = this.gameObject.GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		getOtherNearbyNodesWithTag ("Node");
	}

	private void getOtherNearbyNodesWithTag (string tag)
	{
		List<GameObject> allNodes = new List<GameObject> (GameObject.FindGameObjectsWithTag (tag));
		List<GameObject> otherNodes = new List<GameObject> (allNodes);

		otherNodes.RemoveAll (item => item.name == gameObject.name);

		foreach (GameObject otherNode in otherNodes) {
			float distanceToOtherNode = Vector3.Distance (otherNode.transform.position, gameObject.transform.position);
			float forceMagnitude = 0;
			Vector3 forceToOtherNode;
			Rigidbody otherBody;
			Vector3 toOtherNode;

			if (distanceToOtherNode > 0) {
				forceMagnitude = ((1 / distanceToOtherNode) + 1) * attractionForce;
			}

			if (forceMagnitude > 0) {
				toOtherNode = otherNode.transform.position - gameObject.transform.position;
				forceToOtherNode = toOtherNode * forceMagnitude;
				body.AddForce(forceToOtherNode); 
			}
		}
	}
}
