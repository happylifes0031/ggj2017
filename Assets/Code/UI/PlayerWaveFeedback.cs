using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaveFeedback : MonoBehaviour {
	struct FeedbackWave
	{
		public float speed { get; set; }
		public GameObject obj;
	}
	public float WaveGrowSpeed = 0.3f;

	private GameState gameState;
	private GameObject waveBase;
	private Sprite waveSprite;
	private FeedbackWave feedbackWave;
	private float startTime = 0.0f;
	private bool touchingTheScreen = false;
	private float touchDuration = 0.0f;
	private Vector3 touchPosition;

	void Start () {
		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
		waveSprite = Resources.Load<Sprite>("ClickArt");

		waveBase = new GameObject();
		waveBase.AddComponent<SpriteRenderer>();
		waveBase.SetActive(false);

		SpriteRenderer renderer = waveBase.GetComponent<SpriteRenderer>();
		renderer.sprite = waveSprite;
		renderer.sortingLayerName = "Gameplay";
	}
	
	void Update () {
		if(Input.GetButtonDown("Fire1"))
		{
			touchingTheScreen = true;
			startTime = Time.time;

			touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			feedbackWave = new FeedbackWave();
			feedbackWave.speed = WaveGrowSpeed;

			feedbackWave.obj = Instantiate(waveBase, new Vector3(touchPosition.x, touchPosition.y, 1.0f), Quaternion.identity);

			feedbackWave.obj.SetActive(true);
		}

		if (touchingTheScreen) {
			touchDuration = Mathf.Min(Time.time - startTime, 3.0f);
			float incrementalScale = feedbackWave.speed * touchDuration;

			float xScale = feedbackWave.obj.transform.localScale.x;
			float yScale = feedbackWave.obj.transform.localScale.y;
			feedbackWave.obj.transform.localScale = new Vector3(xScale + incrementalScale, yScale + incrementalScale, 1.0f);
		}

		if (Input.GetButtonUp("Fire1"))
		{
			touchingTheScreen = false;
//			GameState.gameState.playerWaves.AddNewWave (touchPosition, Vector3.up, touchDuration);
			Destroy(feedbackWave.obj);
			touchDuration = 0.0f;
		}
	}
}
