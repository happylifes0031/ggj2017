using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public GameObject ScorePrefab;

	private List<GameObject> nodes;
	private GameObject canvas;
	private Text scoreText;


	// Use this for initialization
	void Start () {
		nodes = Horde.horde.Nodes;
		canvas = GameObject.Find ("Canvas");

		ScorePrefab = Instantiate (ScorePrefab);
		ScorePrefab.transform.SetParent (canvas.transform, false);
		scoreText = ScorePrefab.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		scoreText.text = nodes.Count.ToString();
	}
}
