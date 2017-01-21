using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class screenClickEvent : MonoBehaviour
{
	struct Wave
	{
		public float speed { get; set; }
		public GameObject obj;
	}

	public float waveMultiplier = 1.0f;
	
	private GameObject waveBase;
	private Sprite waveSprite;
	private List<Wave> waves;

	private float startTime = 0.0f;
	
	// Use this for initialization
	void Start () {

		waveSprite = Resources.Load<Sprite>("clickArt");
		
		waveBase = new GameObject();
		waveBase.SetActive(false);
		waveBase.AddComponent<SpriteRenderer>();

		SpriteRenderer renderer = waveBase.GetComponent<SpriteRenderer>();
		renderer.sprite = waveSprite;
		renderer.sortingLayerName = "Gameplay";

		waves = new List<Wave>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
		{
			startTime = Time.time;
			
		}

		if (Input.GetButtonUp("Fire1"))
		{
			float endTime = Time.time;

			float delta = Mathf.Min(endTime - startTime, 3.0f) * waveMultiplier;
			
			Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			
			Wave wave = new Wave();
			wave.speed = delta;
			wave.obj = Instantiate(waveBase, new Vector3(p.x, p.y, 1.0f), Quaternion.identity);
			wave.obj.SetActive(true);
			waves.Add(wave);
		}

		List<Wave> wavesToDelete = new List<Wave>();
		foreach (Wave wave in waves)
		{
			float incrementalScale = wave.speed * Time.deltaTime;

			float xScale = wave.obj.transform.localScale.x;
			float yScale = wave.obj.transform.localScale.y;
			wave.obj.transform.localScale = new Vector3(xScale + incrementalScale, yScale + incrementalScale, 1.0f);

			SpriteRenderer renderer = wave.obj.GetComponent<SpriteRenderer>();

			renderer.color -= new Color(0,0,0,incrementalScale);

			if(renderer.color.a <= 0.0f)
			{
				wavesToDelete.Add(wave);
			}
        }

		foreach(Wave wave in wavesToDelete)
		{
			waves.Remove(wave);
			Destroy(wave.obj);
		}
		
	}
}
