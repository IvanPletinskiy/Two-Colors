using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeDialog : MonoBehaviour {

	public GameObject dialogPlay;

	public Camera mainCamera;

	public static bool isDialog=false;

	void Update(){
		if (isDialog)
			DialogShow ();
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit)) {
				if (hit.collider.tag == "Button") {
					dialogPlay.SetActive (false);
					TilesScript.isDeadFreeze = true;
					Time.timeScale = 1;
				}
			}

		}
	}

	void DialogShow(){
		dialogPlay.SetActive (true);
		TilesScript.isDeadFreeze = false;
		Time.timeScale = 0;
		isDialog = false;
	}



}
