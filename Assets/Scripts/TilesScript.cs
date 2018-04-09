using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TilesScript : MonoBehaviour {

	public Slider slider;
	public RectTransform sliderRect;

	float timer = 0f;

	public GameObject fillArea;

	float[] startColors = new float[3];

    public Text scoreText, levelText;

	public Camera mainCamera;

	float spread = 0.09f;

	float randomColorDouble;
	float randomColorSecond;
	float randomColorLast;

	float fillAreaColor;

	bool isFillColor;
	int numberOfActiveTiles = 0;
   
	public static int score = 0;

	public GameObject[] tiles = new GameObject[4];

    int level = 0;
//    int score = 0;

	bool isTimer=false;

    public bool next, lose;

    void Start () {
		if (!Preferences.isWelcomeShown()) {
			Preferences.setWelcomeShown (true);
			DialogManager.showWelcomeDialog ();
		}
		fillAreaColor = fillArea.GetComponent<Image> ().color.r;
		startColors [0] = fillArea.GetComponent<Image> ().color.r;
		startColors [1] = fillArea.GetComponent<Image> ().color.g;
		startColors [2] = fillArea.GetComponent<Image> ().color.b;
        score = 0;
        next = false;
        lose = false;
        updateLevel();
	}

	void Update () {
		if (isTimer == true && timer > 0f) {
			timer -= Time.deltaTime * 10f;
			
		}
		if (timer <= 40f) {
			if (isFillColor == true) {
				fillArea.GetComponent<Image> ().color = new Vector4(fillArea.GetComponent<Image> ().color.r+ 0.02f,fillArea.GetComponent<Image> ().color.g- 0.02f,fillArea.GetComponent<Image> ().color.b- 0.02f,fillArea.GetComponent<Image> ().color.a);

			}
			else fillArea.GetComponent<Image> ().color = new Vector4(fillArea.GetComponent<Image> ().color.r- 0.02f,fillArea.GetComponent<Image> ().color.g+ 0.02f,fillArea.GetComponent<Image> ().color.b+0.02f,fillArea.GetComponent<Image> ().color.a);

			if (fillArea.GetComponent<Image> ().color.r - fillAreaColor > 0.1f)
				isFillColor = false;
			if (fillAreaColor - fillArea.GetComponent<Image> ().color.r> 0.1f)
				isFillColor = true;
		}
		if (timer <= 0f)
			endGame ();
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
					hit.collider.transform.position = new Vector3(hit.collider.transform.position.x,hit.collider.transform.position.y,-3.44f);
					numberOfActiveTiles++;
					checkNumber ();
				}
				else if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z ==-3.44f) {
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
				if (tiles [i].transform.position.z == -3.44f && tiles[i].GetComponent<Renderer>().material.color == Color.HSVToRGB (randomColorDouble, 1f, 1f)) {
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
		fillArea.GetComponent<Image> ().color = new Vector4(startColors[0],startColors[1],startColors[2],1f);
        level++;
		score += (int)timer;
		timer = 100f;
		isTimer = false;
        scoreText.text = score.ToString();
        string text = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation("level",nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);
        levelText.text = text + " " + level.ToString();
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
