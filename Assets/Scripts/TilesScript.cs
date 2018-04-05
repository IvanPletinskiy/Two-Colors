using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TilesScript : MonoBehaviour {

	public Slider slider;

	float timer = 0f;

    public Text scoreText, levelText;

	public Camera mainCamera;

	float spread = 0.09f;

	float randomColorDouble;
	float randomColorSecond;
	float randomColorLast;

	int numberOfActiveTiles = 0;
   
	public static int score = 0;

	public GameObject[] tiles = new GameObject[4];

    int level = 0;
//    int score = 0;

	bool isTimer=false;

    public bool next, lose;

    void Start () {
        next = false;
        lose = false;
        updateLevel();
	}

	void Update () {
		if (isTimer == true&& timer >0f) {
			timer -= Time.deltaTime * 10f;
			
		}
		slider.value = timer; 
        if (lose)
            endGame();
        if (next && !lose)
            updateLevel();
		if (Input.GetKeyDown (KeyCode.Space))
			updateTiles ();

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			RaycastHit hit;
			Ray ray = mainCamera.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z ==-3.14f ) {
					hit.collider.transform.position = new Vector3(hit.collider.transform.position.x,hit.collider.transform.position.y,-3.7f);
					numberOfActiveTiles++;
					checkNumber ();
				}
				else if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z ==-3.7f) {
					hit.collider.transform.position = new Vector3(hit.collider.transform.position.x,hit.collider.transform.position.y,-3.14f);
					numberOfActiveTiles--;
				}
			}
		}


    }

	private void checkNumber(){
		if (numberOfActiveTiles == 2) {
			int number = 0;
			for (int i = 0; i < 4; i++) {
				if (tiles [i].transform.position.z == -3.7f && tiles[i].GetComponent<Renderer>().material.color == Color.HSVToRGB (randomColorDouble, 1f, 1f)) {
					number++;
				}
			}
			if(number ==2)
				updateLevel();
			else endGame();

		}
	}

    private void updateLevel()
    {
        level++;
		score += (int)timer;
		timer = 100f;
		isTimer = false;
        scoreText.text = score.ToString();
        levelText.text = "Level " + level.ToString();

        updateTiles();
    }

    private void updateTiles()
    {
		for (int i = 0; i < 4; i++) {
			tiles [i].transform.position = new Vector3 (tiles [i].transform.position.x, tiles [i].transform.position.y, -3.14f);
		}
		numberOfActiveTiles = 0;
		randomColorDouble = Random.Range(0f,1f);
		randomColorSecond = randomColorDouble - spread;
		randomColorLast = randomColorDouble + spread;
		int firstTile = Random.Range(0,4);
		int firstDoubleTile= Random.Range(0,4);
		int secondTile= Random.Range(0,4);
		int lastTile= Random.Range(0,4);
		while (firstDoubleTile == firstTile)
			firstDoubleTile = Random.Range (0, 4);
		while (secondTile == firstTile || secondTile == firstDoubleTile)
			secondTile = Random.Range (0, 4);
		while (lastTile == firstTile || lastTile == firstDoubleTile||lastTile == secondTile)
			lastTile = Random.Range (0, 4);
		tiles [firstTile].GetComponent<Renderer>().material.color = Color.HSVToRGB (randomColorDouble, 1f, 1f);
		tiles [firstDoubleTile].GetComponent<Renderer>().material.color =Color.HSVToRGB (randomColorDouble, 1f, 1f);
		tiles [secondTile].GetComponent<Renderer>().material.color =Color.HSVToRGB (randomColorSecond, 1f, 1f);
		tiles [lastTile].GetComponent<Renderer>().material.color=Color.HSVToRGB (randomColorLast, 1f, 1f);
		if (spread > 0.018f)
			spread -= 0.001f;
		isTimer = true;
    }

    private void checkResult()
    {

    }

    private void endGame()
    {
		SceneManager.LoadScene ("Game end");
    }

}
