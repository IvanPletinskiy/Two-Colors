using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences {
	public static string DIALOG_WELCOME = "dialog_welcome";
	public static string DIALOG_RATE = "dialog_rate";
    public static string TOTAL_ATTEMPTS = "total_attempts";
    public static string RECORD = "record";
    public static string AUTHENTICATED = "authenticated";

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
		if (PlayerPrefs.GetInt(DIALOG_WELCOME) != 1)
            return true;
        else
            return false;
    }

    public static void setWelcomeShown(bool isShown)
    {
        if(isShown)
            PlayerPrefs.SetInt(DIALOG_WELCOME, 1);
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

    public static bool isAuthenticated()
    {
        int value = PlayerPrefs.GetInt(AUTHENTICATED, 0);
        return value == 1;
    }

    public static void setAuthenticated(bool isAuthenticated)
    {
        int value = isAuthenticated ? 1 : 0;
        PlayerPrefs.SetInt(AUTHENTICATED, value);
    }
		
}
