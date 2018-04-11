using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespawnScript : MonoBehaviour {

	public Camera mainCam;
	public GameObject heard;
	public static bool isHeard=true;

	float spreadPlay = TilesScript.spread;
	int levelPlay= TilesScript.level;
	int scorePlay= TilesScript.score;

	float randomColorDouble= TilesScript.randomColorDouble;
	float randomColorSecond= TilesScript.randomColorSecond;
	float randomColorLast= TilesScript.randomColorLast;

	int firstTile= TilesScript.firstTile;
	int firstDoubleTile= TilesScript.firstDoubleTile;
	int secondTile= TilesScript.secondTile;
	int lastTile= TilesScript.lastTile;

	void Start () {
		if (isHeard) {
			heard.SetActive (true);
		} else
			heard.SetActive (false);
	}
	void Update () {
		
		
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			RaycastHit hit;
			Ray ray = mainCam.ScreenPointToRay (new Vector2 (Input.mousePosition.x,Input.mousePosition.y));
			print (Physics.Raycast (ray, out hit));
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "Resp") {
					isHeard = false;

					TilesScript.randomColorDouble = randomColorDouble;
					TilesScript.randomColorSecond = randomColorSecond;
					TilesScript.randomColorLast = randomColorLast;

					TilesScript.firstTile = firstTile;
					TilesScript.firstDoubleTile = firstDoubleTile;
					TilesScript.secondTile = secondTile;
					TilesScript.lastTile = lastTile;

					TilesScript.isGenerating = false;
					TilesScript.spread = spreadPlay;
					TilesScript.level = levelPlay;
					TilesScript.score = scorePlay;
					SceneManager.LoadScene ("Play");
				}

			}
		}

	}
}
