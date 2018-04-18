using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogRate : MonoBehaviour {

	public GameObject dialogRate;

	public Camera mainCamera;

	public static bool isDialogRate=false;

	void Update () {
		if (isDialogRate && PlayerPrefs.GetInt("IsRate") != 1)
			DialogRateShow ();
		
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			Ray ray = mainCamera.ScreenPointToRay (new Vector2 (Input.mousePosition.x,Input.mousePosition.y));
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit)) {
				if (hit.collider.tag == "Button") {
					dialogRate.SetActive (false);
					isDialogRate = false;
					Time.timeScale = 1;
				}
				if (hit.collider.name == "Yes") {
					Application.OpenURL ("https://play.google.com/store/apps/details?id=com.appsforluck.richandsuccessful");
				}
				if (hit.collider.name == "No, thanks"){
					PlayerPrefs.SetInt ("IsRate", 1);
					dialogRate.SetActive (false);
					isDialogRate = false;
					Time.timeScale = 1;
				}
			}
		}
	}

	void DialogRateShow(){
		dialogRate.SetActive (true);
		Time.timeScale = 0;
	}
}
