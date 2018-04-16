﻿using System.Collections;
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

    public AudioClip clip;
    AudioSource audioSource;


    void Start()
    {
        initializeAd();
        audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(clip);
                gameObject.transform.localScale = new Vector3 (0.34f, 0.34f, 1f);
			    StartCoroutine ("wait");
                break;
        }
    }

    public void initializeAd()
    {
        string appKey = "b1312497ddd5c9fdc3ba969a9488d90b5278eb4b1f8c0a22";
        Appodeal.initialize(appKey, Appodeal.NON_SKIPPABLE_VIDEO);
    }

	IEnumerator wait(){
		yield return new WaitForSeconds (0.1f);
		SceneManager.LoadScene("Play");
		gameObject.transform.localScale = new Vector3(0.3f,0.3f,1f);
	}


}
