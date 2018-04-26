using UnityEngine;

public class Preferences
{
    public static string DIALOG_WELCOME = "dialog_welcome";
    public static string DIALOG_RATE = "dialog_rate";
    public static string TOTAL_ATTEMPTS = "total_attempts";
    public static string RECORD = "record";
    public static string AUTHENTICATED = "authenticated";
    public static string MUSIC = "music";

    public static void increaseAndSaveAttempts()
    {
		PlayerPrefs.SetInt(TOTAL_ATTEMPTS, getAttempts() + 1);
    }

    public static int getAttempts()
    {
        return PlayerPrefs.GetInt(TOTAL_ATTEMPTS, 0);
    }

    //Используется по нажатию на кнопку Later в диалоге Rate
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
        if (isShown)
            PlayerPrefs.SetInt(DIALOG_WELCOME, 1);
    }

    public static bool isRateShown()
    {

		if (PlayerPrefs.GetInt(DIALOG_RATE, 0) == 1)
            return true;
        else
            return false;
    }

    public static void setRateShown(bool isShown)
    {
        if (isShown)
            PlayerPrefs.SetInt(DIALOG_RATE, 1);
        else
            PlayerPrefs.SetInt(DIALOG_RATE, 0);

    }

    public static bool isAuthenticated()
    {
		return PlayerPrefs.GetInt(AUTHENTICATED, 0) == 1;
    }

    public static void setAuthenticated(bool isAuthenticated)
    {
		PlayerPrefs.SetInt(AUTHENTICATED, isAuthenticated ? 1 : 0);
    }

    public static bool isMusic()
    {
        if (PlayerPrefs.GetInt(MUSIC) != 1)
            return true;
        else
            return false;
    }

    public static void setMusic(bool isMus)
    {
        if (isMus)
            PlayerPrefs.SetInt(MUSIC, 1);
        else
            PlayerPrefs.SetInt(MUSIC, 0);
    }

}