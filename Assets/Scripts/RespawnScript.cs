using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using AppodealAds.Unity.Common;
using AppodealAds.Unity.Api;

public class RespawnScript : MonoBehaviour, INonSkippableVideoAdListener
{
    public Text scoreText, recordText;

    public AudioClip gameOverClip;
    public AudioClip gameRecordClip;

    public Text newRecord;
    //bool isRecord = false;

    public GameObject adButton;

    public Camera mainCam;
    public GameObject heart;
    //	public static bool isHeard = Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO);

    float spreadPlay = TilesScript.spread;
    int levelPlay = TilesScript.level;
    int scorePlay = TilesScript.score;

    float randomColorDouble = TilesScript.randomColorDouble;
    float randomColorSecond = TilesScript.randomColorSecond;
    float randomColorLast = TilesScript.randomColorLast;

    int firstTile = TilesScript.firstTile;
    int firstDoubleTile = TilesScript.firstDoubleTile;
    int secondTile = TilesScript.secondTile;
    int lastTile = TilesScript.lastTile;

    private string adCallbackTAG; // Используется в коллбеке рекламы для определения был просмотрен ролик по нажатию на сердечко или на кнопку

    void Start()
    {
        Time.timeScale = 1;
        scoreText.text = TilesScript.score.ToString();

        if (Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO))
        {
            heart.SetActive(true);
            adButton.gameObject.SetActive(true);
        }
        else
        {
            adButton.gameObject.SetActive(false);
        }

        if (TilesScript.score > PlayerPrefs.GetInt("Record"))
        {
            handleRecord();
        }
        else
        {
            string text = "";
            text = nl.DTT.LanguageManager.Managers.AbstractLanguageManager.GetTranslation("yourRecord",
                nl.DTT.LanguageManager.Managers.AbstractLanguageManager.CurrentLanguage);
            text += PlayerPrefs.GetInt("Record").ToString();
            recordText.text = text;
            newRecord.gameObject.SetActive(false);
            recordText.gameObject.SetActive(true);
            if (Preferences.isMusic())
                GetComponent<AudioSource>().PlayOneShot(gameOverClip);
            Preferences.increaseAndSaveAttempts();
            if (Preferences.getAttempts() >= DialogRate.ATTEMPTSFORDIALOG)
            {
                DialogRate.DialogRateShow();
            }
        }

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TilesScript.score = 0;
            TilesScript.level = 1;
            SceneManager.LoadScene("Main Menu");
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            print(Physics.Raycast(ray, out hit));
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Resp")
                {
                    adCallbackTAG = "HEART";
                    showAd();
                }
                if (hit.collider.name == "pass")
                {
                    heart.SetActive(false);
                    //					Start ();
                }

                if (hit.collider.name == "RestartButton")
                {
                    SceneManager.LoadScene("Play");
                    //isHeard = true;
                    TilesScript.isGenerating = true;
                    TilesScript.spread = 0.09f;
                    TilesScript.level = 1;
                    TilesScript.score = 0;
                }
                if (hit.collider.name == "HomeButton")
                {
                    SceneManager.LoadScene("Main menu");
                    //isHeard = true;
                    TilesScript.isGenerating = true;
                    TilesScript.spread = 0.09f;
                    TilesScript.level = 1;
                    TilesScript.score = 0;
                }
                if (hit.collider.name == "AdButton")
                {
                    adCallbackTAG = "ADBUTTON";
                    showAd();
                }
            }
        }
    }

    private void handleRecord()
    {
        PlayerPrefs.SetInt("Record", TilesScript.score);
        //     isRecord = true;
        //       DialogRate.isDialogRate = true;
        if (Preferences.isMusic())
            GetComponent<AudioSource>().PlayOneShot(gameRecordClip);
        newRecord.gameObject.SetActive(true);
        recordText.gameObject.SetActive(false);
        postRecordInLeaderboard();
    }


    private void postRecordInLeaderboard()
    {
        //if (Social.localUser.authenticated)
        {
            Social.ReportScore(TilesScript.score, "CgkInY7b68gcEAIQAA", (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");
                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }

    private void showAd()
    {
        Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
    }

    #region Rewarded Video callback handlers
    public void onNonSkippableVideoClosed()
    {
        if (adCallbackTAG.Equals("HEART"))
        {
            //           isHeard = false;
            heart.SetActive(false);

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
            SceneManager.LoadScene("Play");
        }

        if (adCallbackTAG.Equals("ADBUTTON"))
        {
            TilesScript.score = (int)(TilesScript.score * 1.5);
            adButton.SetActive(false);
            scoreText.text = TilesScript.score.ToString();
            string text = "";
            if (TilesScript.score > PlayerPrefs.GetInt("record"))
            {
                handleRecord();
            }
        }
        /*

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
                */

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