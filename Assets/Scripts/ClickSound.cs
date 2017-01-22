using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickSound :  MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  {
	private bool hovered;
	public AudioClip sound;

	private Button button { get { return GetComponent<Button> (); } }
	private AudioSource source { get { return GetComponent<AudioSource> (); } } 

	void Start() { 
		hovered = false;
		gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		source.playOnAwake = false;
	}

	public void OnPointerEnter (PointerEventData eventData) 
	{
		Debug.Log ("The cursor entered the selectable UI element.");
		source.PlayOneShot(sound);
		transform.localScale += new Vector3(0.1f, 0.1f, 0);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		transform.localScale -= new Vector3(0.1f, 0.1f, 0);
		hovered = false;
	}
}
