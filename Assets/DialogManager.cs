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
        MNRateUsPopup rateUs = new MNRateUsPopup("rate us", "rate us, please", "Rate Us", "No, Thanks", "Later");
        rateUs.SetAppleId(dummyAppleId);
        rateUs.SetAndroidAppUrl(dummyAndroidAppUrl);
        rateUs.AddDeclineListener(() => { Debug.Log("rate us declined"); });
        rateUs.AddRemindListener(() => { Debug.Log("remind me later"); });
        rateUs.AddRateUsListener(() => { Debug.Log("rate us!!!"); });
        rateUs.AddDismissListener(() => { Debug.Log("rate us dialog dismissed :("); });
        rateUs.Show();
    }

    public static void showWelcomeDialog()
    {
        MNPopup popup = new MNPopup("title", "Welcome");
        popup.AddAction("Ok", () => { Debug.Log("Ok action callback"); });
        popup.AddDismissListener(() => { Debug.Log("dismiss listener"); });
        popup.Show();
    }

}
