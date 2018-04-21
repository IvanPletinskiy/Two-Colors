using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using AppodealAds.Unity.Api;
//using AppodealAds.Unity.Common;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class MainSceneButtonListener : MonoBehaviour
{
    public Text recordText;

    public string action;

    void Start()
    {
		//PlayerPrefs.SetInt(Preferences.DIALOG_WELCOME, 0);//ВАНЯ, ЭТО ДЛЯ ТОГО ЧТО БЫ ПРОВЕРЯТЬ ДИАЛОГ НА РАБОТОСПОСОБНОСТЬ
		//PlayerPrefs.SetInt ("IsRate", 0);//ТОЛЬКО ДЛЯ ТЕСТА
//		PlayerPrefs.SetInt ("onlyOneDialog", 0);//ТОЛЬКО ДЛЯ ТЕСТА
        initializeAd();
        WelcomeDialog.isDialog = false;
        Time.timeScale = 1;
        recordText.text = PlayerPrefs.GetInt("Record").ToString();
    }

    void Awake()
    {
        initializeGPS();
    }


    void OnMouseDown()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnMouseUp()
    {

    }

    void OnMouseUpAsButton()
    {
		print ("Yes");
		switch (gameObject.name)
        {
		    case "Play":
				print ("Yesa");
				GetComponent<AudioSource> ().Play();
                gameObject.transform.localScale = new Vector3(0.34f, 0.34f, 1f);
                StartCoroutine("wait");
                WelcomeDialog.isDialog = true;
                WelcomeDialog.isActive = true;
                break;
			case "ShowLeaderboard" :
				Debug.Log("Inside ShowLeaderboard");
                gameObject.transform.localScale = new Vector3(0.21f, 0.21f, 1f);
                StartCoroutine("leaderboardButtonCourite");
                PlayGamesPlatform.Instance.ShowLeaderboardUI();
                /*
                if (Social.localUser.authenticated)
                    Social.ShowLeaderboardUI();
                else
                {
                    // authenticate user:
                    Social.localUser.Authenticate((bool success) => {
                        // handle success or failure
                        if (success)
                        {
                            Debug.Log("Login Success....");
                     //       PostHighScoreOnLeaderBoard();
                            Social.ShowLeaderboardUI();
                        }
                        else
                        {
                            Debug.Log("Login Failed....");
                        }
                    });
                }
                */
                break;	
        }
    }

    public void initializeAd()
    {
        //string appKey = "b1312497ddd5c9fdc3ba969a9488d90b5278eb4b1f8c0a22";
        //Appodeal.initialize(appKey, Appodeal.NON_SKIPPABLE_VIDEO);
    }

    public void initializeGPS()
    {
        if (!Preferences.isAuthenticated())
        {
            // Рекомендовано для откладки:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Активировать Google Play Games Platform
            PlayGamesPlatform.Activate();
            // Аутентификация игрока:
            Social.localUser.Authenticate((bool isAuthenticated) =>
            {            
                Preferences.setAuthenticated(isAuthenticated);
            });
        }

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Play");
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
    }
    IEnumerator leaderboardButtonCourite()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.localScale = new Vector3(0.174f, 0.174f, 1f);
    }
}
