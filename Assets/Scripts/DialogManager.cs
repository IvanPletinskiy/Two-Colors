using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager
{
    public static int startAttempts = 20;
    public static int nedeedScore = 25;
    public static int delayAttempts = 15;


    public const string dummyAppleId = "itms-apps://itunes.apple.com/id375380948?mt=8";
    public const string dummyAndroidAppUrl = "market://details?id=com.google.earth";

    public static void showRateDialog()
    {
		Debug.Log ("showRateDialog");
        
        MNRateUsPopup rateUs = new MNRateUsPopup("rate us", "rate us, please", "Rate Us", "No, Thanks", "Later");
        rateUs.SetAppleId(dummyAppleId);
        rateUs.SetAndroidAppUrl(dummyAndroidAppUrl);
        rateUs.AddDeclineListener(() => { Debug.Log("rate us declined"); });
        rateUs.AddRemindListener(() => { Debug.Log("remind me later"); });
        rateUs.AddRateUsListener(() => { Debug.Log("rate us!!!"); });
		rateUs.AddDismissListener(() => { PlayerPrefs.SetInt("IsRate",1); });
        rateUs.Show();
        
    }

    public static void showWelcomeDialog()
    {
        Debug.Log("showWelcomeDialog");
        
        MNPopup popup = new MNPopup("title", "Welcome");
		popup.AddAction("Ok", () => { Time.timeScale=1; });
        popup.AddDismissListener(() => { Debug.Log("dismiss listener"); });
		Time.timeScale=0;
        popup.Show();
    }
}
