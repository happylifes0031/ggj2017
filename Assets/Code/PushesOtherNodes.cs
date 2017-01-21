using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class PushesOtherNodes : MonoBehaviour
{
	public float PushForce = 0.06f;
	public float MaximumPushDistance = 4f;

	void Update ()
	{
		pushOtherNodesWithTag ("Node");
	}

	private void pushOtherNodesWithTag (string tag)
	{
		List<GameObject> allNodes = new List<GameObject> (GameObject.FindGameObjectsWithTag (tag));

		foreach (GameObject node in allNodes) {
			float distanceToOtherNode = Vector3.Distance (node.transform.position, gameObject.transform.position);
			float forceMagnitude = 0;

			if (distanceToOtherNode > 0 && distanceToOtherNode < MaximumPushDistance) {
				Rigidbody otherBody = node.GetComponent<Rigidbody> ();
				Vector3 toOtherNode = node.transform.position - gameObject.transform.position;
				forceMagnitude = ((1 / distanceToOtherNode) + 1) * PushForce;
				Vector3 impulseToOtherNode = toOtherNode * forceMagnitude;

				if (otherBody != null) {
					otherBody.AddForce (impulseToOtherNode);
				}
			}
		}
	}
}
