using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
	public GameObject shepard;
	public GameObject wolf;

	private GameObject shepardPrefab;
	private GameObject wolfPrefab;

	public List<GameObject> BadShepards { get; private set; }
	public List<GameObject> WolfInSheepsClothes { get; private set; }

	void preloadShepard()
	{
		shepardPrefab = Instantiate(shepard);
		shepardPrefab.SetActive(false);
		shepardPrefab.layer = 8;
		Rigidbody2D rigidBody = shepardPrefab.AddComponent<Rigidbody2D>();
		rigidBody.gravityScale = 0.0f;
		CircleCollider2D sphereCollider = shepardPrefab.AddComponent<CircleCollider2D>();
		sphereCollider.radius = 0.015f;
	}

	void preloadWolf()
	{
		wolfPrefab = Instantiate(wolf);
		wolfPrefab.SetActive(false);
		wolfPrefab.layer = 8;
		Rigidbody2D rigidBody = wolfPrefab.AddComponent<Rigidbody2D>();
		rigidBody.gravityScale = 0.0f;
		CircleCollider2D sphereCollider = wolfPrefab.AddComponent<CircleCollider2D>();
		sphereCollider.radius = 0.015f;
	}

	void AddNewShepard(Vector3 centerOfHorde)
	{

	}

	void AddNewWolf(Vector3 centerOfHorde)
	{									 
		float xRand = (Random.value * 2f) - 1f;
		float yRand = (Random.value * 2f) - 1f;

		Vector3 spawnPosWorld = centerOfHorde + (new Vector3(xRand, yRand)).normalized * 40.0f;

		GameObject wolf = Object.Instantiate(wolfPrefab, spawnPosWorld, Quaternion.identity);
		wolf.SetActive(true);

		WolfInSheepsClothes.Add(wolf);
	}

	void Start ()
	{
		BadShepards = new List<GameObject>();
		WolfInSheepsClothes = new List<GameObject>();

		preloadWolf();

		preloadShepard();
		
	}

	Vector3 randomMovement = Vector3.zero;
	void Update ()
	{
		Vector3 flockCenter = GameState.gameState.horde.CenterOfHorde;

		// spawn new enemies
		if(Input.GetKeyDown(KeyCode.A))
		{
			AddNewShepard(flockCenter);
		}

		// spawn new enemies
		if (Input.GetKeyDown(KeyCode.B))
		{
			AddNewWolf(flockCenter);
		}

		foreach (GameObject wolf in WolfInSheepsClothes)
		{
			float distance = Vector3.Distance(flockCenter, wolf.transform.position);

			float speedMul = Mathf.SmoothStep(8.0f, .6f, Mathf.Clamp(distance, 0f, 12f) / 12.0f);

			Vector3 direction = flockCenter - wolf.transform.position;
			wolf.transform.position += direction.normalized * Time.deltaTime * 10.0f * speedMul;
			
			randomMovement += direction * Random.value;

			float colorScale = Mathf.SmoothStep(1.0f, 0.0f, Mathf.Clamp(distance, 0f, 12f) / 12.0f);
			MeshRenderer meshRenderer = wolf.GetComponent<MeshRenderer>();
			meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, new Color(1, 0.0f, 0.0f, 1f), colorScale);

			wolf.transform.position += randomMovement.normalized * 15f * Time.deltaTime;

			if(distance < 2.0f)
			{
				CircleCollider2D circle = wolf.GetComponent<CircleCollider2D>();
				//circle.radius = 10.0f;
			}
		}
	}
}
