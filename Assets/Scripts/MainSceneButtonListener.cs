using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class MainSceneButtonListener : MonoBehaviour {

	public GameObject quad;

    public Text recordText;

    public string action;

    void Start()
    {
        initializeAd();    
   //     Preferences.resetAttempts();
		Preferences.setWelcomeShown(false);
		recordText.text = PlayerPrefs.GetInt("Record").ToString();
    }


	void OnMouseDown()
    {

    }

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

    void OnMouseUp()
    {

    }

    void OnMouseUpAsButton()
    {
		
        switch (action)
        {
		case "Play":
			SceneManager.LoadScene("Play");
            break;
        }
    }

    public void initializeAd()
    {
        string appKey = "b1312497ddd5c9fdc3ba969a9488d90b5278eb4b1f8c0a22";
        Appodeal.initialize(appKey, Appodeal.NON_SKIPPABLE_VIDEO);
    }

}
