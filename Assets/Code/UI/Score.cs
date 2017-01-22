using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public GameObject ScorePrefab;
	public int initialFontSize = 90;
	public float panicColorShiftSpeed = 0.4f;
	public int panicFontSizeIncrease = 5;

	private float panicLevel = 0f;
	private List<GameObject> nodes;
	private GameObject canvas;
	private Text scoreText;
	private int previousScore;

	// Use this for initialization
	void Start () {
		nodes = Horde.horde.Nodes;
		canvas = GameObject.Find ("Canvas");

		ScorePrefab = Instantiate (ScorePrefab);
		ScorePrefab.transform.SetParent (canvas.transform, false);
		scoreText = ScorePrefab.GetComponent<Text> ();
		scoreText.fontSize = initialFontSize;
	}

	// Update is called once per frame
	void Update () {
		int currentScore = nodes.Count;

		scoreText.text = currentScore.ToString();


		if (currentScore < previousScore) {
			panicLevel = updatePanicLevel (currentScore);

			Color currentColor = scoreText.color;
			Color aBitMoreRed = new Color (
				                   panicLevel * panicColorShiftSpeed, 
				                   -1 * (panicLevel * panicColorShiftSpeed), 
				                   -1 * (panicLevel * panicColorShiftSpeed
				                   ), 0f);
			currentColor += aBitMoreRed;

			scoreText.color = currentColor;
		}

		if (currentScore % 10 == 0) {
			scoreText.fontSize += panicFontSizeIncrease;
		}

		previousScore = currentScore;
	}

	float updatePanicLevel(int score) {
		float result = 0f;

		if (score > 0) {
			result = 1f / score;

			return result;
		} else {
			return 1f;
		}
	}
}
