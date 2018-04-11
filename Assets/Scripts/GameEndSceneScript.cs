﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndSceneScript : MonoBehaviour {
    public Text scoreText, recordText;

	void Start () {
		checkAndShowRateDialog ();
		scoreText.text = TilesScript.score.ToString();

        string text = "";

        Preferences.increaseAndSaveAttempts();
        if (Preferences.getRecord () < TilesScript.score) {

            text  = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation("newRecord", 
                nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);

            
			Preferences.setRecord (TilesScript.score);
		} 
		else {
            text = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation("yourRecord",
                nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);
            text += Preferences.getRecord().ToString();
        }
			
        recordText.text = text;
	}

	void Update () {
        
    }

	void OnMouseUpAsButton() {
		switch (gameObject.name) {
		case "RestartButton":
			SceneManager.LoadScene ("Play");
			RespawnScript.isHeard = true;
			TilesScript.isGenerating = true;
			TilesScript.spread = 0.09f;
			TilesScript.level = 1;
			TilesScript.score = 0;
			break;
		case "HomeButton":
			SceneManager.LoadScene ("Main menu");
			RespawnScript.isHeard = true;
			TilesScript.isGenerating = true;
			TilesScript.spread = 0.09f;
			TilesScript.level = 2;
			TilesScript.score = 0;
			break;
		}
	}

	public void Isclick(){
		
	}

	void checkAndShowRateDialog() {
		if (Preferences.getAttempts () >= DialogManager.startAttempts &&
		   TilesScript.score >= DialogManager.nedeedScore)
			DialogManager.showRateDialog ();
			
	}

}
