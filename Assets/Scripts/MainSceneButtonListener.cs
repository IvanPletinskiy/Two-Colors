using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneButtonListener : MonoBehaviour {

	public GameObject quad;

    public Text recordText;

    public string action;

    void Start()
    {

	
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

}
