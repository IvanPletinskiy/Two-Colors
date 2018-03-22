using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTiles : MonoBehaviour {
	public Material[] colors = new Material[4];
	public GameObject[] tiles = new GameObject[4];

	public Canvas inGame;
	public Canvas gameOver;

	public Text textDead;

	bool isDead = false;

	public Text textScore;

	public Slider slider;
	public Text timerText;

	public Canvas startGame;

	public Text textLvl;

	bool[] colorsNumber = new bool[4];

	int lvlScore=0;

	float spread=0.05f;

	public Camera mainCamera;

	int count = 0;

	float timer = 100;
	bool timerCheck = false;

	void GenerateTiles () {
		
		isDead = false;

		timerCheck = true;

		timer = 100;

		for (int i = 0; i < 4; i++) {
			colorsNumber [i] = true;
			tiles [i].transform.position = new Vector3 (tiles [i].transform.position.x, tiles [i].transform.position.y, -3.47f);
		}
		count = 0;
			
		float randomColor = Random.Range(0f,1f);
		float randomColor3 = randomColor - spread;
		float randomColor4 = randomColor + spread;
		int firstTile = Random.Range (1, 5);
		int secondTile = Random.Range (1, 5);
		while(secondTile == firstTile)
			secondTile = Random.Range (1, 5);
		colors [firstTile-1].color = Color.HSVToRGB (randomColor3, 1f, 1f);
		colors [secondTile-1].color = Color.HSVToRGB (randomColor4, 1f, 1f);
		colorsNumber [firstTile - 1] = false;
		colorsNumber [secondTile - 1] = false;
		for (int i = 0; i < 4; i++) {
			if (colorsNumber [i] == true)
				colors [i].color = Color.HSVToRGB (randomColor, 1f, 1f);
		}
		textLvl.text = "" + lvlScore;

		if (spread > 0.019f)
			spread -= 0.001f;
	}
    void onGameEnded(){
		spread = 0.05f;
		timerCheck = false;
		if (isDead == false)
			textDead.text = "Вы выбрали не те цвета";
		else textDead.text = "Время закончилось";
		textScore.text = textLvl.text;
		mainCamera.transform.position = new Vector3 (20f, mainCamera.transform.position.y, mainCamera.transform.position.z);
		gameOver.enabled = true;
		inGame.enabled = false;
	}

	void Update () {
		if (timer > 0 && timerCheck == true)
			timer -=Time.deltaTime;
		if (timer <= 0) {
			isDead = true;
			timer = 100;
			onGameEnded ();
		}
		timerText.text = "" + (int)timer;
		slider.value = timer;
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			Ray ray = mainCamera.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "Tiles") {
					if (hit.collider.transform.position.z == -3.47f) {
						hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -4f);
						count++;
					} else if (hit.collider.transform.position.z == -4f) {
						hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -3.47f);
						count--;
					}

					if (count == 2)
						checkSelected();
					
				}
				if (hit.collider.tag == "EndGame") {
					mainCamera.transform.position = new Vector3 (50f, mainCamera.transform.position.y, mainCamera.transform.position.z);
					startGame.enabled = true;
					gameOver.enabled = false;
					lvlScore = 0;
				}
				if (hit.collider.tag == "PlayGame") {
					mainCamera.transform.position = new Vector3 (0f, mainCamera.transform.position.y, mainCamera.transform.position.z);
					inGame.enabled = true;
					startGame.enabled = false;
					lvlScore = 0;
					GenerateTiles ();
				}
			}
		}

	}
	void checkSelected () {
		int countTrue = 0;
		for (int i = 0; i < 4; i++) {
			if (tiles [i].transform.position.z == -4 && colorsNumber [i] == true)
				countTrue++;
		}
		if (countTrue == 2) {
			lvlScore +=(int) timer;
			GenerateTiles ();
		}
			
		else
			onGameEnded ();
	}
}
