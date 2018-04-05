using nl.DTT.LanguageManager.SceneObjects;
using nl.DTT.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemoUIManager : MonoBehaviour {
    /// <summary>
    /// DropDown for selecting Language
    /// </summary>
    [SerializeField]
    private Dropdown languageDropdown;

	/// <summary>
    /// Loads Languages into Dropdown
    /// </summary>
	void Awake() {
        languageDropdown.ClearOptions();
        List<Dropdown.OptionData> l = new List<Dropdown.OptionData>();
        foreach (Language lang in Enum.GetValues(typeof(Language)))
            l.Add(new Dropdown.OptionData(lang.ToString()));
        languageDropdown.AddOptions(l);
        StartCoroutine(SetDropDown());
    }

    private IEnumerator SetDropDown()
    {
        yield return new WaitUntil(() => LanguageManager.IsLoaded());
        languageDropdown.value = ((Language[])Enum.GetValues(typeof(Language))).ToList().IndexOf(LanguageManager.CurrentLanguage);
        languageDropdown.onValueChanged.AddListener(SetLanguageFromDropdown);
    }

    /// <summary>
    /// Sets Language from Dropdown (OnValueChanged handler)
    /// </summary>
    public void SetLanguageFromDropdown(int newVal)
    {
        string selectedLanguage = languageDropdown.options[newVal].text;
        if (Enum.IsDefined(typeof(Language), selectedLanguage))
            LanguageManager.SetLanguage((Language)Enum.Parse(typeof(Language), selectedLanguage));
        else
            Debug.LogError("Unable to parse selected Language: " + selectedLanguage);
    }
    /// <summary>
    /// Updates all registered Texts
    /// </summary>
    public void UpdateRegisteredTexts()
    {
        LanguageManager.RefreshRegisteredTranslatedTexts();
    }
    /// <summary>
    /// Updates ALL TranslatedText objects
    /// </summary>
    public void UpdateAllTexts()
    {
        TranslatedText.RefreshAll();
    }
}