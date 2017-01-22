using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour {

	public void NewGameBtn(string newGameLevel){
		SceneManager.LoadScene (newGameLevel);
	}

	public void ExitGameBtn(){
		Application.Quit ();
	}
}
	