using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Application.LoadLevel("Play");
                break;
        }
    }
}
