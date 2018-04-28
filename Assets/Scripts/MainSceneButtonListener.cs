using AppodealAds.Unity.Api;
using GooglePlayGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainSceneButtonListener : MonoBehaviour
{
    public Text recordText;

	public GameObject offMusic;

    public string action;

    void Start()
    {
        //PlayerPrefs.SetInt(Preferences.DIALOG_WELCOME, 0);//ВАНЯ, ЭТО ДЛЯ ТОГО ЧТО БЫ ПРОВЕРЯТЬ ДИАЛОГ НА РАБОТОСПОСОБНОСТЬ
        //PlayerPrefs.SetInt (Preferences.DIALOG_RATE, 1);//ТОЛЬКО ДЛЯ ТЕСТА
        //		PlayerPrefs.SetInt ("onlyOneDialog", 0);//ТОЛЬКО ДЛЯ ТЕСТА
        //    Preferences.resetAttempts();
        //    Preferences.setRateShown(false);
        //   AndroidDialogAndToastBinding.instance.toastShort("TOAST");
        // Рекомендовано для откладки:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Активировать Google Play Games Platform
        PlayGamesPlatform.Activate();
        initializeAd();
        initializeGPS();
        WelcomeDialog.isDialog = false;
        Time.timeScale = 1;
        recordText.text = PlayerPrefs.GetInt("Record").ToString();
    }

    void Update()
    {
		offMusic.SetActive (!Preferences.isMusic ());
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "OpenMarket":
                Application.OpenURL("https://play.google.com/store/apps/details?id=com.handen.twocolors");
                break;
            case "Music":
                Preferences.setMusic(Preferences.isMusic());
                print(Preferences.isMusic());
                break;
            case "Play":
                print("Yesa");
                if (Preferences.isMusic())
                    GetComponent<AudioSource>().Play();
                gameObject.GetComponent<Animator>().enabled = false;
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 0.1f, gameObject.transform.localScale.y + 0.1f, 1f);
                StartCoroutine("wait");
                break;
            case "ShowLeaderboard":
                //               gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
                //               StartCoroutine("leaderboardButtonCourite");
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    showToast("checkConnection");
                }
                else
                {
                    if (Social.localUser.authenticated)
                    {
                        Social.ShowLeaderboardUI();
                        Social.ReportScore(TilesScript.score, "CgkInY7b68gcEAIQAA", (bool success) => {});
                    }
                    else
                    {
                        showToast("authorizing");
                        Social.localUser.Authenticate((bool isAuthenticated) =>
                        {
                            if (isAuthenticated)
                            { 
                                Social.ShowLeaderboardUI();
                                Social.ReportScore(TilesScript.score, "CgkInY7b68gcEAIQAA", (bool success) => {});
                            }
                            else
                                showToast("leaderboardError");
                        });
                    }

                }
                break;
        }
    }

    public void showToast(string tag)
    {
        string toastText = nl.DTT.LanguageManager.Managers.AbstractLanguageManager.GetTranslation(tag,
                           nl.DTT.LanguageManager.Managers.AbstractLanguageManager.CurrentLanguage);

        AndroidDialogAndToastBinding.instance.toastShort(toastText);
    }

    public void initializeAd()
    {
        Appodeal.disableLocationPermissionCheck();
        Appodeal.disableWriteExternalStoragePermissionCheck();
        if (!Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO))
        {
            //if(Appodeal.isLoaded())
            Appodeal.disableNetwork("inmobi");
            string appKey = "b1312497ddd5c9fdc3ba969a9488d90b5278eb4b1f8c0a22";
            Appodeal.initialize(appKey, Appodeal.NON_SKIPPABLE_VIDEO);
        }
    }

    public void initializeGPS()
    {
        if (!Preferences.isAuthenticated())
        {
            // Рекомендовано для откладки:
    //        PlayGamesPlatform.DebugLogEnabled = true;
            // Активировать Google Play Games Platform
      //      PlayGamesPlatform.Activate();
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
		gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
    }
    IEnumerator leaderboardButtonCourite()
    {
        yield return new WaitForSeconds(0.1f);
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
