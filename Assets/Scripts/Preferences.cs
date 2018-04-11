using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences {
	public static string DIALOG_WELCOME = "dialog_welcome";
	public static string DIALOG_RATE = "dialog_rate";
    public static string TOTAL_ATTEMPTS = "total_attempts";
    public static string RECORD = "record";

    public static void increaseAndSaveAttempts()
    {
        int playerAttempts = getAttempts() + 1;
        PlayerPrefs.SetInt(TOTAL_ATTEMPTS, playerAttempts);
    }

    public static int getAttempts()
    {
        return PlayerPrefs.GetInt(TOTAL_ATTEMPTS, 0);
    }

    public static void resetAttempts()
    {
        PlayerPrefs.SetInt(TOTAL_ATTEMPTS, 0);
    }

    public static bool isWelcomeShown()
    {
        int value = PlayerPrefs.GetInt(DIALOG_WELCOME, 0);
        return value == 1;
    }

    public static void setWelcomeShown(bool isShown)
    {
        int value = isShown ? 1 : 0;
        PlayerPrefs.SetInt(DIALOG_WELCOME ,value);
    }

    public static bool isRateShown()
    {
        int value = PlayerPrefs.GetInt(DIALOG_RATE, 0);
        return value == 1;
    }

    public static void setRateShown(bool isShown)
    {
        int value = isShown ? 1 : 0;
        PlayerPrefs.SetInt(DIALOG_RATE, value);
    }
		
}
