using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image image;
    [SerializeField] private Sprite isOnSprite;
    [SerializeField] private Sprite isOffSprite;
    [SerializeField] private AppSettings appSettings;
    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        appSettings = FindObjectOfType<AppSettings>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        toggle = GetComponent<Toggle>();
        image = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        if (appSettings.isFullScreen)
        {
            image.sprite = isOnSprite;
        }
        else
        {
            image.sprite = isOffSprite;
        }
        Screen.fullScreen = appSettings.isFullScreen;
    }

    public void ClickToggle()
    {
        audioManager.PlayTapSound();
        ToggleEvent();
    }

    public void ToggleEvent()
    {
        if (toggle.isOn)
        {
            image.sprite = isOnSprite;
            Screen.fullScreen = true;
            appSettings.isFullScreen = true;
        }
        else
        {
            image.sprite = isOffSprite;
            Screen.fullScreen = false;
            appSettings.isFullScreen = false;
        }
    }
}
