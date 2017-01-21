using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	
	float lerp = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (Input.GetMouseButton(1))
		{
			lerp = Mathf.Min(lerp + Time.deltaTime,1.0f);

			Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

			Vector3 centerPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

			Vector3 delta = centerPos - newPos;

			Vector3 lerpVec = new Vector3(Mathf.Lerp(0, delta.x, lerp), Mathf.Lerp(0, delta.y, lerp), 0.0f);

			this.transform.position += lerpVec;
		}
		else
		{
			lerp = 1.0f;
        }
	}
}
