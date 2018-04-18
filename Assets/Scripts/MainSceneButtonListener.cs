using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MainSceneButtonListener : MonoBehaviour
{


    public Text recordText;

    public string action;
<<<<<<< HEAD
=======

    public AudioClip clip;
    AudioSource audioSource;
>>>>>>> f1457651b0642b7acd43fff9583720ebaf4a1147

    void Awake()
    {
        initializeAd();
        initializeGPS();
    }


    void Start()
    {
<<<<<<< HEAD
		PlayerPrefs.SetInt ("onlyOneDialog", 0);//ТОЛЬКО ДЛЯ ТЕСТА
        initializeAd();
=======
        audioSource = GetComponent<AudioSource>();
>>>>>>> f1457651b0642b7acd43fff9583720ebaf4a1147
        WelcomeDialog.isDialog = false;
        Time.timeScale = 1;
        Preferences.setWelcomeShown(false);
        recordText.text = PlayerPrefs.GetInt("Record").ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
		case "Play":
				GetComponent<AudioSource> ().Play();
                gameObject.transform.localScale = new Vector3(0.34f, 0.34f, 1f);
                StartCoroutine("wait");
                WelcomeDialog.isDialog = true;
                WelcomeDialog.isActive = true;
                break;
            case "ShowLeaderboard":
                Debug.Log("ShowLeaderboard Button");
                PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkInY7b68gcEAIQAA");
                break;
        }
    }

    public void initializeAd()
    {
//        string appKey = "b1312497ddd5c9fdc3ba969a9488d90b5278eb4b1f8c0a22";
//        Appodeal.initialize(appKey, Appodeal.NON_SKIPPABLE_VIDEO);
    }

    public void initializeGPS()
    {
        // Рекомендовано для откладки:
        PlayGamesPlatform.DebugLogEnabled = true;
        
        if(!Social.localUser.authenticated)
            // Аутентификация игрока:
            Social.localUser.Authenticate((bool success) => {
                // Удачно или нет?
            
            });

        // Активировать Google Play Games Platform
        PlayGamesPlatform.Activate();
    }

IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Play");
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
    }
}
