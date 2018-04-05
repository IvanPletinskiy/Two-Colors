using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndSceneScript : MonoBehaviour {
    public Text scoreText, recordText;

	void Start () {
		scoreText.text = TilesScript.score.ToString ();

		if (Preferences.getRecord () < TilesScript.score) {
			recordText.text = "New Record!";
			Preferences.setRecord (TilesScript.score);
		} 
		else
			recordText.text = "Your record is " + Preferences.getRecord ().ToString ();
	}

	void Update () {
		
	}

	void onMouseDownAsButton(){
		switch (gameObject.name) {
		case "RestartButton":
			SceneManager.LoadScene ("Play");
			break;
		case "HomeButton":
			SceneManager.LoadScene ("Main menu");
			break;
		}
	}
}
