using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScore : MonoBehaviour {
	public GameObject TimePrefab;
	public int FontSize = 90;

	private GameObject timeScore;
	private GameObject canvas;
	private Text timeText;

	void Start () {
		canvas = GameObject.Find ("Canvas");

		TimePrefab = Instantiate (TimePrefab);
		TimePrefab.transform.SetParent (canvas.transform, false);
		timeText = TimePrefab.GetComponent<Text> ();
	}

	void Update () {
		timeText.text = Horde.horde.TotalTime.ToString();
	}

}
