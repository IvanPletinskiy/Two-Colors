using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Common;
using AppodealAds.Unity.Api;

public class GameEndSceneScript : MonoBehaviour, INonSkippableVideoAdListener {
    public Text scoreText, recordText;

	public Text newRecord;
	bool isRecord = false;

	void Start () {
        Debug.Log("GameEndScript is loaded " + Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO));
        scoreText.text = TilesScript.score.ToString();

        string text = "";
		if (TilesScript.score > PlayerPrefs.GetInt ("Record")) {
			PlayerPrefs.SetInt ("Record", TilesScript.score);
			text = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation ("yourRecord",
				nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);
			text += PlayerPrefs.GetInt ("Record").ToString ();
			recordText.text = text;
			print ("NEW RECORD");
			isRecord = true;
			DialogManager.showRateDialog ();

		} else {
			text = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation ("yourRecord",
				nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);
			text += PlayerPrefs.GetInt ("Record").ToString ();
			recordText.text = text;
			isRecord = false;
		}
		newRecord.gameObject.SetActive (isRecord);
        
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
#region Rewarded Video callback handlers
    public void onNonSkippableVideoClosed()
    {

    }

    public void onNonSkippableVideoFailedToLoad()
    {

    }

    public void onNonSkippableVideoFinished()
    {

    }

    public void onNonSkippableVideoLoaded()
    {

    }

    public void onNonSkippableVideoShown()
    {

    }
#endregion
}
