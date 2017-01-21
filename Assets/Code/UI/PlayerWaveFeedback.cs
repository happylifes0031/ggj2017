using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaveFeedback : MonoBehaviour {
	struct FeedbackWave
	{
		public float speed { get; set; }
		public GameObject obj;
	}

	// Make sure there's only one PlayerWaveFeedback. Use it with:
	// PlayerWaveFeedback.playerWaveFeedback.ShowFeedback();
	public static PlayerWaveFeedback playerWaveFeedback;
	public float WaveGrowSpeed = 0.3f;
	public float BlinkDuration = 0.17f;
	public float PulseSpeed = 4.0f;

	private GameObject waveBase;
	private Sprite waveSprite;
	private FeedbackWave feedbackWave;
	private float startTime = 0.0f;
	private bool touchingTheScreen = false;
	private float touchDuration = 0.0f;
	private Vector3 touchPosition;
	private float maxDuration = 3.0f;

	void Awake() {
		playerWaveFeedback = this;
	}

	void Start () {
		waveSprite = Resources.Load<Sprite>("ClickArt");

		waveBase = new GameObject();
		waveBase.AddComponent<SpriteRenderer>();
		waveBase.SetActive(false);

		SpriteRenderer feedbackRenderer = waveBase.GetComponent<SpriteRenderer>();
		feedbackRenderer.sprite = waveSprite;
		feedbackRenderer.sortingLayerName = "Gameplay";
	}

	public float incrementalVal;
	void Update () {
		if (touchingTheScreen) {
			touchDuration = Mathf.Min(Time.time - startTime, maxDuration);

			if (touchDuration < maxDuration) {
				incrementalVal = 0f;
				float incrementalScale = feedbackWave.speed * touchDuration;

				float xScale = feedbackWave.obj.transform.localScale.x;
				float yScale = feedbackWave.obj.transform.localScale.y;
				feedbackWave.obj.transform.localScale = new Vector3 (xScale + incrementalScale, yScale + incrementalScale, 1.0f);
			} else {
				incrementalVal += Time.deltaTime * PulseSpeed;
				float alpha = 0.5f * (Mathf.Sin (incrementalVal) + 1f);

				SpriteRenderer feedbackRenderer = feedbackWave.obj.GetComponent<SpriteRenderer> ();
				feedbackRenderer.color *= new Color (1f, 1f, 1f, 0f);
				feedbackRenderer.color += new Color (0f,0f,0f, alpha);
			}
		}
	}

	public void ShowFeedback(float maxInputDuration) {
		maxDuration = maxInputDuration;
		touchingTheScreen = true;
		startTime = Time.time;

		touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
		feedbackWave = new FeedbackWave();
		feedbackWave.speed = WaveGrowSpeed;

		feedbackWave.obj = Instantiate(waveBase, new Vector3(touchPosition.x, touchPosition.y, 1.0f), Quaternion.identity);

		feedbackWave.obj.SetActive(true);
	}

	public void HideFeedback() {
		touchingTheScreen = false;
		touchDuration = 0.0f;
		Destroy(feedbackWave.obj);

		return;
	}
}
