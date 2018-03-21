using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс отвечающий за показ диалогов, сначала вызови getInstance(), потом методы
public class DialogManager
{

    public const string dummyAppleId = "itms-apps://itunes.apple.com/id375380948?mt=8";
    public const string dummyAndroidAppUrl = "market://details?id=com.google.earth";

    private static DialogManager manager;

    private DialogManager()
    {

    }

    public static DialogManager getInstance()
    {
        if (manager == null)
            manager = new DialogManager();

        return manager;
    }

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

}
