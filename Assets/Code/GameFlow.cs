using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
	public float maxLifeTimeSeconds = 5.0f;
	private float startTimeSeconds = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{

		// start press
		if(Input.GetButtonDown("Fire1"))
		{
			startTimeSeconds = Time.time;
		}

		// end press and spawn wave
		if (Input.GetButtonUp("Fire1"))
		{
			float endTimeSeconds = Time.time;
			float delta = Mathf.Min(endTimeSeconds - startTimeSeconds, maxLifeTimeSeconds);
			
			Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			Vector3 normal = Vector3.up;

			GameState.gameState.playerWaves.AddNewWave(position, normal, delta);
		}

		ResolveNodes();
    }

	void ResolveInput()
	{

	}

	void ResolveNodes()
	{
		return;

		List<GameObject> allNodes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Node"));
		foreach (PlayerWave wave in GameState.gameState.playerWaves.Waves)
		{
			SphereCollider waveCollider = wave.Obj.GetComponent<SphereCollider>();

			foreach(GameObject gameObject in allNodes)
			{

			}
		}
	}
}
