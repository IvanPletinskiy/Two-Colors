using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using AppodealAds.Unity.Common;
//using AppodealAds.Unity.Api;

public class RespawnScript : MonoBehaviour {//, INonSkippableVideoAdListener
	public Text scoreText, recordText;

    public AudioClip gameOverClip;

    public Text newRecord;
	bool isRecord = false;

	string text;

	public GameObject adButton;

	public Camera mainCam;
	public GameObject heard;
	public static bool isHeard=true;

	float spreadPlay = TilesScript.spread;
	int levelPlay= TilesScript.level;
	int scorePlay= TilesScript.score;

	float randomColorDouble= TilesScript.randomColorDouble;
	float randomColorSecond= TilesScript.randomColorSecond;
	float randomColorLast= TilesScript.randomColorLast;

	int firstTile = TilesScript.firstTile;
	int firstDoubleTile = TilesScript.firstDoubleTile;
	int secondTile = TilesScript.secondTile;
	int lastTile = TilesScript.lastTile;


	void Start () {
		Time.timeScale = 1;

        GetComponent<AudioSource>().PlayOneShot(gameOverClip);

        scoreText.text = TilesScript.score.ToString();

		text = "";
		if (TilesScript.score > PlayerPrefs.GetInt ("Record")) {
			PlayerPrefs.SetInt ("Record", TilesScript.score);
			text = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation ("yourRecord",
				nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);
			text += PlayerPrefs.GetInt ("Record").ToString ();
			recordText.text = text;
			print ("NEW RECORD");
			isRecord = true;
			DialogRate.isDialogRate = true;

		}
        else {
			text = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation ("yourRecord",
				nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);
			text += PlayerPrefs.GetInt ("Record").ToString ();
			recordText.text = text;
			isRecord = false;
		}
		newRecord.gameObject.SetActive (isRecord);
	}
	void Update () {

		if (isHeard) {
			heard.SetActive (true);
			newRecord.gameObject.SetActive (false);
		}
		else
			heard.SetActive (false);
        /*if (Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO)) {
            adButton.gameObject.SetActive(true);
        }
        else {
            adButton.gameObject.SetActive(false);
        }*/

		if (Input.GetKeyDown(KeyCode.Escape)) {
			TilesScript.score = 0;
			TilesScript.level = 1;

			SceneManager.LoadScene ("Main Menu");
		}

		
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			RaycastHit hit;
			Ray ray = mainCam.ScreenPointToRay (new Vector2 (Input.mousePosition.x,Input.mousePosition.y));
			print (Physics.Raycast (ray, out hit));
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "Resp") {
					/*isHeard = false;

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
					SceneManager.LoadScene ("Play");*/
					showAd ();
				}
				if (hit.collider.name == "pass") {
					isHeard = false;
					print ("Yes");
				}

				if (hit.collider.name == "RestartButton") {
					SceneManager.LoadScene ("Play");
					isHeard = true;
					TilesScript.isGenerating = true;
					TilesScript.spread = 0.09f;
					TilesScript.level = 1;
					TilesScript.score = 0;
				}
				if (hit.collider.name == "HomeButton") {
					SceneManager.LoadScene ("Main menu");
					isHeard = true;
					TilesScript.isGenerating = true;
					TilesScript.spread = 0.09f;
					TilesScript.level = 1;
					TilesScript.score = 0;
				}
                if(hit.collider.name == "AdButton")
                {
                    showAdXScore();
                }
			}
		}
	}
	private void showAdXScore(){
		
	}
    private void showAd()
    {
        //Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);

		print ("Ad");
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

    #region Rewarded Video callback handlers
    public void onNonSkippableVideoClosed()
    {
        TilesScript.score = (int)(TilesScript.score * 1.5);

        scoreText.text = TilesScript.score.ToString();
        string text = "";

        if (TilesScript.score > PlayerPrefs.GetInt("record"))
        {
            PlayerPrefs.SetInt("record", TilesScript.score);
            text = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation("yourRecord",
                nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);
            text += PlayerPrefs.GetInt("record").ToString();
            recordText.text = text;
            print("NEW RECORD");
            isRecord = true;
            DialogManager.showRateDialog();
        }
        else
        {
            text = nl.DTT.LanguageManager.SceneObjects.LanguageManager.GetTranslation("yourRecord",
                nl.DTT.LanguageManager.SceneObjects.LanguageManager.CurrentLanguage);
            text += PlayerPrefs.GetInt("Record").ToString();
            recordText.text = text;
            isRecord = false;
        }

    }

    public void onNonSkippableVideoFailedToLoad()
    {

    }

    public void onNonSkippableVideoFinished()
    {

    }

    public void onNonSkippableVideoLoaded()
    {

    }

    public void onNonSkippableVideoShown()
    {

    }
    #endregion
}
