using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButtonListener : MonoBehaviour {

    public string action;

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
