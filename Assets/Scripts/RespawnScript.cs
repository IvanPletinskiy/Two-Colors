using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnScript : MonoBehaviour {

	public Camera mainCamera;

	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			print ("Da");
			RaycastHit hit;
			Ray ray = mainCamera.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
			if (Physics.Raycast (ray, out hit)) {
				print ("Da1");
				SceneManager.LoadScene ("Play");
			}
		}

	}
}
