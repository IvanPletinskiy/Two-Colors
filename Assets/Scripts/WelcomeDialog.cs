using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeDialog : MonoBehaviour {

	public GameObject dialogPlay;


	void DialogShow(){
		dialogPlay.SetActive (true);
		TilesScript.isDeadFreeze = false;
	}



}
