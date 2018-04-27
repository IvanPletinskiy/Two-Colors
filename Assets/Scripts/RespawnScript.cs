using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject dialogRate;
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

    private static bool isHeartShown = false;

    private static string adCallbackTAG; // Используется в коллбеке рекламы для определения был просмотрен ролик по нажатию на сердечко или на кнопку

    void Start()
    {
        Appodeal.setNonSkippableVideoCallbacks(this);
        Time.timeScale = 1;
        scoreText.text = TilesScript.score.ToString();

        if (Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO))
        {
            if (!isHeartShown && Random.Range(0, 3) == 0)
            {
                heart.SetActive(true);
                isHeartShown = true;
            }
               
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
  //          if(heart.activeInHierarchy)
            newRecord.gameObject.SetActive(false);
            recordText.gameObject.SetActive(true);
            if (Preferences.isMusic() && !heart.activeInHierarchy)
                GetComponent<AudioSource>().PlayOneShot(gameOverClip);
            Preferences.increaseAndSaveAttempts();
        }
		if (Preferences.getAttempts() >= DialogRate.ATTEMPTSFORDIALOG && Preferences.isRateShown() &&
			!heart.activeInHierarchy)
		{
			//      DialogRate.DialogRateShow();
			dialogRate.SetActive(true);
			// Preferences.setRateShown(true);
			print("aaaa");
			print(Preferences.isRateShown());
		}
    }

    private void handleRecord()
    {
        PlayerPrefs.SetInt("Record", TilesScript.score);
        if (!heart.activeInHierarchy)
        {
            newRecord.gameObject.SetActive(true);
            if (Preferences.isMusic())
                GetComponent<AudioSource>().PlayOneShot(gameRecordClip);
        }
        recordText.gameObject.SetActive(false);
        postRecordInLeaderboard();
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
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Resp")
                {
                    isHeartShown = true;
                    showAd();
                    StartCoroutine("heartCoroutine");
                }
                if (hit.collider.name == "pass")
                {
                    isHeartShown = true;
                    heart.SetActive(false);
                    //                   TilesScript.score = scorePlay;
                    if (scorePlay >= PlayerPrefs.GetInt("Record"))
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
                        //          if(heart.activeInHierarchy)
                        newRecord.gameObject.SetActive(false);
                        recordText.gameObject.SetActive(true);
                        if (Preferences.isMusic())
                            GetComponent<AudioSource>().PlayOneShot(gameOverClip);
                        Preferences.increaseAndSaveAttempts();
                       
                    }
					if (Preferences.getAttempts() >= DialogRate.ATTEMPTSFORDIALOG && Preferences.isRateShown() &&
						!heart.activeInHierarchy)
					{
						DialogRate.DialogRateShow();
						Preferences.setRateShown(true);
						print("aa");
					}
                   
                }
                if (hit.collider.name == "RestartButton")
                {
                    SceneManager.LoadScene("Play");
                    isHeartShown = false;
                    //isHeard = true;
					hit.collider.transform.localScale = new Vector3(hit.collider.transform.localScale.x+0.1f, hit.collider.transform.localScale.y+0.1f, 1f);
					StartCoroutine (waitForScale (hit.collider.gameObject));
                    TilesScript.isGenerating = true;
                    TilesScript.spread = 0.09f;
                    TilesScript.level = 1;
                    TilesScript.score = 0;
                }
                if (hit.collider.name == "HomeButton")
                {
                    SceneManager.LoadScene("Main menu");
                    //isHeard = true;
					hit.collider.transform.localScale = new Vector3(hit.collider.transform.localScale.x+0.1f, hit.collider.transform.localScale.y+0.1f, 1f);
                    TilesScript.isGenerating = true;
					StartCoroutine (waitForScale (hit.collider.gameObject));
                    TilesScript.spread = 0.09f;
                    TilesScript.level = 1;
                    TilesScript.score = 0;
                }
                if (hit.collider.name == "AdButton")
                { 
                    showAd();
					hit.collider.transform.localScale = new Vector3(hit.collider.transform.localScale.x+0.1f, hit.collider.transform.localScale.y+0.1f, 1f);
                    StartCoroutine("adButtonCoroutine");
					StartCoroutine (waitForScale (hit.collider.gameObject));
                }
               
                if (hit.collider.name == "Yes")
                {
                    Application.OpenURL("https://play.google.com/store/apps/details?id=com.handen.twocolors");
                    dialogRate.SetActive(false);
                    Preferences.setRateShown(false);
                }
				else if (hit.collider.name == "No, thanks")
                {
					print (Preferences.isRateShown ());
					Preferences.setRateShown(false);
					print (Preferences.isRateShown ());
                    dialogRate.SetActive(false);
                    Time.timeScale = 1;
                }
					else  if (hit.collider.tag == "Button")
					{
						print ("DA");
						dialogRate.SetActive(false);
						Preferences.resetAttempts();
						Time.timeScale = 1;
					}
            }
        }
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

	IEnumerator waitForScale(GameObject button){
		yield return new WaitForSeconds (0.1f);
		button.transform.localScale = new Vector3 (1f, 1f, 1f);
	}

    IEnumerator heartCoroutine()
    {
        yield return new WaitForSeconds(0.3f);

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
        adCallbackTAG = "HEART";

        heart.SetActive(false);

    }
    IEnumerator adButtonCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        adCallbackTAG = "ADBUTTON";
        TilesScript.score = (int)(TilesScript.score * 1.5);
        adButton.SetActive(false);
        scoreText.text = TilesScript.score.ToString();

        if (TilesScript.score > PlayerPrefs.GetInt("Record"))
        {
            handleRecord();
        }
    }  

    #region Rewarded Video callback handlers
    public void onNonSkippableVideoClosed()
    {
  //      SceneManager.LoadScene("Main Menu");
        /*
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