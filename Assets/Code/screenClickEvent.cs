using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenClickEvent : MonoBehaviour {

	public float waveSpeed = 1.0f;
	
	private GameObject waveBase;
	private Sprite waveSprite;
	private List<GameObject> waves;
	

	// Use this for initialization
	void Start () {

		waveSprite = Resources.Load<Sprite>("clickArt");


		waveBase = new GameObject();
		waveBase.AddComponent<SpriteRenderer>();

		SpriteRenderer renderer = waveBase.GetComponent<SpriteRenderer>();
		renderer.sprite = waveSprite;
		renderer.sortingLayerName = "Gameplay";

		waves = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
		{
			Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			
			int waveIndex = waves.Count;
			waves.Add(Instantiate(waveBase, new Vector3(p.x, p.y, 1.0f), Quaternion.identity));
		}

		foreach (GameObject wave in waves)
		{
			float incrementalScale = waveSpeed * Time.deltaTime;

			float xScale = wave.transform.localScale.x;
			float yScale = wave.transform.localScale.y;
			wave.transform.localScale = new Vector3(xScale + incrementalScale, yScale + incrementalScale, 1.0f);
		}
	}
}
