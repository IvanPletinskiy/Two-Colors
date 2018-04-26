using AppodealAds.Unity.Api;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TilesScript : MonoBehaviour{ //, INonSkippableVideoAdListener

	public Slider slider;
	public RectTransform sliderRect;

	public AudioClip clickSound;
	public AudioClip moneySound;
	public AudioClip gameOverSound;

	float timer = 0f;

	public GameObject fillArea;

	public GameObject TEST; // DON'T DELETE

	float[] startColors = new float[3];

	public Text scoreText, levelText;

	public Camera mainCamera;

	public static float spread = 0.09f;

	public GameObject wrongTiles;
	public GameObject referWrongTiles;

	public static bool isDeadFreeze = true;

	public static float randomColorDouble;
	public static float randomColorSecond;
	public static float randomColorLast;

	public static int firstTile;
	public static int firstDoubleTile;
	public static int secondTile;
	public static int lastTile;

	float fillAreaColor;

	bool isFillColor;
	int numberOfActiveTiles = 0;

	public static int score = 0;

	public GameObject[] tiles = new GameObject[4];

	public static int level = 0;
	//    int score = 0;

	//	float fadeDead=0f;

	bool isMultitouch;
	bool isOnetouch;

	bool isTimer = false;

	public static bool isGenerating=true;

	public bool next, lose;

	void Start () {
		WelcomeDialog.isActive = true;
		WelcomeDialog.isDialog = true;
		Time.timeScale = 1;
		level--;
		//		if (!Preferences.isWelcomeShown()) {
		//			Preferences.setWelcomeShown (true);
		//			DialogManager.showWelcomeDialog ();
		//		}
		fillAreaColor = fillArea.GetComponent<Image> ().color.r;
		startColors [0] = fillArea.GetComponent<Image> ().color.r;
		startColors [1] = fillArea.GetComponent<Image> ().color.g;
		startColors [2] = fillArea.GetComponent<Image> ().color.b;
		next = false;
		lose = false;
		updateLevel();

		for (int i = 0; i < 4; i++) {
			tiles [i].transform.position = new Vector3 (tiles [i].transform.position.x, tiles [i].transform.position.y, -0.89f);
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && WelcomeDialog.isActive == true)
		{
			WelcomeDialog.isActive = false;
			TilesScript.isDeadFreeze = true;
			Time.timeScale = 1;
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				score = 0;
				level = 1;
				SceneManager.LoadScene("Main Menu");
			}
		}


		if (isTimer == true && timer > 0f) {
			timer -= Time.deltaTime * 10f;
		}

		//fadeDead += Time.deltaTime;
		//	if (fadeDead >= 0.2f) {
		//		referWrongTiles.SetActive (!referWrongTiles.activeSelf);
		//		fadeDead = 0f;
		//	}

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

		/*
        Touch[] touches = Input.touches;
        if (Input.touchCount > 0)
            SceneManager.LoadScene("Main menu");
        if (touches.Length == 0 || isDeadFreeze)
            return;*/
		if (Input.touchCount == 1 && isDeadFreeze) {
			if (isOnetouch) {
				RaycastHit hit;
				Ray ray = mainCamera.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
				if (Physics.Raycast (ray, out hit)) {
					playSound (clickSound);
					if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z == -0.89f) {
						hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -1.36f);
						numberOfActiveTiles++;
						checkNumber ();
					}
					else 
						if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z == -1.36f) {
							hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -0.89f);
							numberOfActiveTiles--;  
						}   
				}
				isOnetouch = false;
			}

		}
		else if (Input.touchCount > 1 && isDeadFreeze) {
			Touch[] touches = Input.touches;
			if (isMultitouch) {
				if (!isOnetouch) {
					for (int i = 1; i < Input.touchCount; i++) {
						Ray ray = mainCamera.ScreenPointToRay (touches [i].position);
						RaycastHit hit;
						if (Physics.Raycast (ray, out hit)) {
							playSound (clickSound);
							if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z == -0.89f) {
								hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -1.36f);
								numberOfActiveTiles++;
								checkNumber ();
							} else if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z == -1.36f) {   
								hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -0.89f);
								numberOfActiveTiles--;
							}
						}
					}
				} else {
					for (int i = 0; i < Input.touchCount; i++) {
						Ray ray = mainCamera.ScreenPointToRay (touches [i].position);
						RaycastHit hit;
						if (Physics.Raycast (ray, out hit)) {
							playSound (clickSound);
							if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z == -0.89f) {
								hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -1.36f);
								numberOfActiveTiles++;
								checkNumber ();
							} else if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z == -1.36f) {   
								hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -0.89f);
								numberOfActiveTiles--;
							}
						}
					}
				}


				isMultitouch = false;
			} 

		}
		else if (isDeadFreeze) {
			isMultitouch = true;
			isOnetouch = true;
			print ("MulriRestore");
		}
		//  else 
		/*if (Input.GetKeyDown (KeyCode.Mouse0) && isDeadFreeze) {
			    RaycastHit hit;
			    Ray ray = mainCamera.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
			    if (Physics.Raycast (ray, out hit)) {
    				playSound (clickSound);
				    if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z == -0.89f) {
    					hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -1.36f);
					    numberOfActiveTiles++;
					    checkNumber ();
				    }
                    else 
                        if (hit.collider.tag == "Tiles" && hit.collider.transform.position.z == -1.36f) {
    					    hit.collider.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, -0.89f);
					        numberOfActiveTiles--;  
				        }   
			}
		}*/

	}


	private void checkNumber() {
		if (numberOfActiveTiles == 2) {
			int number = 0;
			for (int i = 0; i < 4; i++) {
				if (tiles [i].transform.position.z == -1.36f && tiles[i].GetComponent<Renderer>().material.color == Color.HSVToRGB (randomColorDouble, 0.8f, 1f)) {
					number++;
				}
			}
			if (number == 2) {
				updateLevel();
				playSound(moneySound);
			}
			else
			{
				wrongTiles.SetActive(true);
				isDeadFreeze = false;
				isTimer = false;
				StartCoroutine("wait");
			}
		}
	}

	private void updateLevel() {
		wrongTiles.SetActive (false);
		fillArea.GetComponent<Image> ().color = new Vector4(startColors[0],startColors[1],startColors[2],1f);
		level++;
		score += (int)timer;
		scoreText.text = score.ToString();
		string text = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation("level", nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);
		levelText.text = text + " " + level.ToString();
		timer = 100.3f;
		isTimer = false;
		updateTiles();
	}

	private void updateTiles() {
		TEST.gameObject.SetActive (false); // DON'T DELETE

		numberOfActiveTiles = 0;
		if (isGenerating) {
			lastTile = Random.Range (0, 4);
			secondTile = Random.Range (0, 4);
			firstDoubleTile = Random.Range (0, 4);
			firstTile = Random.Range (0, 4);

			randomColorDouble = Random.Range (0f, 1f);
			randomColorSecond = randomColorDouble - spread;
			randomColorLast = randomColorDouble + spread;

			while (firstDoubleTile == firstTile)
				firstDoubleTile = Random.Range (0, 4);
			while (secondTile == firstTile || secondTile == firstDoubleTile)
				secondTile = Random.Range (0, 4);
			while (lastTile == firstTile || lastTile == firstDoubleTile || lastTile == secondTile)
				lastTile = Random.Range (0, 4);
		} else
			isGenerating = true;

		tiles [firstTile].GetComponent<Renderer>().material.color = Color.HSVToRGB (randomColorDouble, 0.8f, 1f);
		tiles [firstDoubleTile].GetComponent<Renderer>().material.color =Color.HSVToRGB (randomColorDouble, 0.8f, 1f);
		tiles [secondTile].GetComponent<Renderer>().material.color =Color.HSVToRGB (randomColorSecond, Random.Range(0.7f,0.8f), 1f);
		tiles [lastTile].GetComponent<Renderer>().material.color=Color.HSVToRGB (randomColorLast, Random.Range(0.7f,0.8f), 1f);
		if (spread > 0.014f)
			spread -= 0.003f;
		isTimer = true;
		for (int i = 0; i < 4; i++) {
			tiles [i].transform.position = new Vector3 (tiles [i].transform.position.x, tiles [i].transform.position.y, -0.89f);
		}
		isDeadFreeze = false;
		StartCoroutine (waitForTouch ());
	}

	IEnumerator waitForTouch(){
		yield return new WaitForSeconds (0.3f);
		isDeadFreeze = true;
	}

	private void playSound(AudioClip clip)
	{
		if (Preferences.isMusic())
			GetComponent<AudioSource>().PlayOneShot(clip);
	}

	private void endGame()
	{
		//        if (Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO))
		//           showHeart();
		//        else
		SceneManager.LoadScene ("Game end");
	}

	private void showAd()
	{
		Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
	}

	IEnumerator wait (){
		yield return new WaitForSeconds (0.5f);
		isDeadFreeze = true;
		endGame ();

	}
	
}