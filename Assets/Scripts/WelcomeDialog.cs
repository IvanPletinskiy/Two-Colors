using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeDialog : MonoBehaviour {

	public GameObject dialogPlay;

	public GameObject TEST;

	public Camera mainCamera;

	public static bool isDialog=false;
	public static bool isActive=true;

	void Update(){
		dialogPlay.SetActive (isActive);
		if (isDialog)
			DialogWelcomeShow ();
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			Ray ray =mainCamera.ScreenPointToRay (new Vector2 (Input.mousePosition.x,Input.mousePosition.y));
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit)) {
				if (hit.collider.tag == "Button") {
					isActive = false;

					TilesScript.isDeadFreeze = true;
					Time.timeScale = 1;
					TEST.SetActive (false);
				}
			}
		}
	}

	void DialogWelcomeShow(){
		dialogPlay.SetActive (isActive);
		TilesScript.isDeadFreeze = false;
		Time.timeScale = 0;
		isDialog = false;
	}
}
