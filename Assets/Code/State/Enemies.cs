using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
	public float Timer = 0;
	public float MaximumTimer = 10.0f;
	public GameObject Obj;
}

public class Enemies : MonoBehaviour
{
	public AudioClip ExplosionSound;
	private AudioSource source;
	public float MaxTimeAlive = 30.0f;
	float newSpawnTime = 0;
	float spawnTimer = 0;

	public GameObject shepard;
	public GameObject wolf;

	private GameObject shepardPrefab;
	private GameObject wolfPrefab;

	public List<GameObject> BadShepards { get; private set; }
	public List<Wolf> WolfInSheepsClothes { get; private set; }

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
		sphereCollider.radius = 0.01f;
	}
	
	void AddNewWolf(Vector3 centerOfHorde)
	{									 
		float xRand = (Random.value * 2f) - 1f;
		float yRand = (Random.value * 2f) - 1f;

		Vector3 spawnPosWorld = centerOfHorde + (new Vector3(xRand, yRand)).normalized * 140.0f;

		GameObject wolf = Object.Instantiate(wolfPrefab, spawnPosWorld, Quaternion.identity);
		wolf.SetActive(true);

		Wolf w = new Wolf();
		w.Obj = wolf;
		w.MaximumTimer = MaxTimeAlive;

		WolfInSheepsClothes.Add(w);
	}

	void Start ()
	{
		BadShepards = new List<GameObject>();
		WolfInSheepsClothes = new List<Wolf>();

		source = gameObject.AddComponent<AudioSource>();

		preloadWolf();
		
	}

	Vector3 randomMovement = Vector3.zero;
	void Update ()
	{
		List<Wolf> wolfToDelete = new List<Wolf>();
		foreach (Wolf w in WolfInSheepsClothes)
		{
			if (w.Timer >= 2.0f || w.MaximumTimer <= 0.0f)
			{
				wolfToDelete.Add(w);
            }
		}

		foreach (Wolf w in wolfToDelete)
		{
			DestroyObject(w.Obj);
			WolfInSheepsClothes.Remove(w);
		}

		Vector3 flockCenter = GameState.gameState.horde.CenterOfHorde;
		spawnTimer += Time.deltaTime;
		if (spawnTimer > newSpawnTime)
		{
			spawnTimer -= newSpawnTime;
			newSpawnTime = 0.1f + Random.value * 2.0f;
			AddNewWolf(flockCenter);
		}

		foreach (Wolf w in WolfInSheepsClothes)
		{
			GameObject wolf = w.Obj;

			float randomMovementMul = 1.0f;
			Vector3 target = flockCenter;
			if(GameState.gameState.horde.Nodes.Count < 10)
			{
				target = GameState.gameState.horde.Nodes[0].transform.position;
				randomMovementMul = 0;
            }

			float distance = Vector3.Distance(flockCenter, wolf.transform.position);

			float speedMul = Mathf.SmoothStep(8.0f, .6f, Mathf.Clamp(distance, 0f, 12f) / 12.0f);

			Vector3 direction = flockCenter - wolf.transform.position;
			wolf.transform.position += direction.normalized * Time.deltaTime * 10.0f * speedMul;
			wolf.transform.localScale = new Vector3(200, 200, 200);

			randomMovement += direction * Random.value * randomMovementMul;

			float colorScale = Mathf.SmoothStep(1.0f, 0.0f, Mathf.Clamp(distance, 0f, 12f) / 12.0f);
			MeshRenderer meshRenderer = wolf.GetComponent<MeshRenderer>();
			meshRenderer.material.color = new Color(1, 0.0f, 0.0f, w.MaximumTimer / MaxTimeAlive);
			
			wolf.transform.position += randomMovement.normalized * 15f * Time.deltaTime;

			if(distance < 2.0f)
			{
				w.Timer += Time.deltaTime;
				
				if(w.Timer >= 2.0f || w.MaximumTimer <= 0.0f)
				{
					CircleCollider2D collider = wolf.GetComponent<CircleCollider2D>();
					collider.radius *= 5.0f;
					wolf.tag = "Wolf";

					//source.PlayOneShot(ExplosionSound);
                }
			}

			w.MaximumTimer -= Time.deltaTime;
		}
	}

}
