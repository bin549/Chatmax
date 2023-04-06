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
    public AudioManager audioManager;
    [SerializeField] private List<LocalizationText> localizationTexts;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        appSettings = FindObjectOfType<AppSettings>();
        audioManager = FindObjectOfType<AudioManager>();
        localizationTexts = FindObjectsOfType<LocalizationText>().ToList();
    }

    private void OnMouseDown()
    {
        audioManager.PlayTapSound();
    }

    private void Start()
    {
        dropdown.value = appSettings.isEnglish ? 1 : 0;
        SwitchLanguage();
    }

    public void FlagSwitch()
    {
        appSettings.isEnglish = dropdown.value == 1 ? true : false;
        audioManager.PlayTapSound();
        SwitchLanguage();
    }

    public void SwitchLanguage()
    {
        foreach (LocalizationText localizationText in localizationTexts)
        {
            localizationText.gameObject.GetComponent<TMP_Text>().text = dropdown.value == 1 ? localizationText.englishText : localizationText.chineseText;
        }
    }
}
