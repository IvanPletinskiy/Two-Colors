﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneButtonListener : MonoBehaviour {

    public Text recordText;

    public string action;

    void Start()
    {
        recordText.text = Preferences.getRecord().ToString();
    }


	void OnMouseDown()
    {

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
}
