using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LocalizationHelper : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public AppSettings appSettings;

    [SerializeField] private List<LocalizationText> localizationTexts;


    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        appSettings = FindObjectOfType<AppSettings>();
        localizationTexts = FindObjectsOfType<LocalizationText>().ToList();
    }

    private void Start()
    {
        if (appSettings.isEnglish) {
            dropdown.value = 1;
        } 
        else
        {
            dropdown.value = 0;
        }
        SwitchLanguage();
    }

    public void FlagSwitch()
    {
        if (dropdown.value == 1)
        {
            appSettings.isEnglish = true;
        }
        else
        {
            appSettings.isEnglish = false;
        }
        SwitchLanguage();
    }

    public void SwitchLanguage()
    {
        foreach (LocalizationText localizationText in localizationTexts)
        {
            if (dropdown.value == 1)
            {
                localizationText.gameObject.GetComponent<Text>().text = localizationText.englishText;
            }
            else
            {
                localizationText.gameObject.GetComponent<Text>().text = localizationText.chineseText;
            }
        }
    }
}
