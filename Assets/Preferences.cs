using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences {
	public static string DIALOG_WELCOME = "dialog_welcome";
	public static string DIALOG_RATE = "dialog_rate";

	public static int getBool(string key) {
		return PlayerPrefs.GetInt (key);
	}

	public static void setBool(string key, bool value) {
		PlayerPrefs.SetInt (key, (int) value);
	}

	public static int getInt(string key) {
		
	}

}
