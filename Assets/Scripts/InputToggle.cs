using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image image;
    [SerializeField] private AppSettings appSettings;

    private void Awake()
    {
        appSettings = GameObject.FindObjectOfType<AppSettings>();
        toggle = GetComponent<Toggle>();
        image = transform.parent.gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        if (appSettings.isUsingKeyboard)
        {
            if (this.gameObject.name == "Keyboard")
            {
                toggle.isOn = true;
            }
            else
            {
                toggle.isOn = false;
            }
        }
        else
        {
            if (this.gameObject.name == "Microphone")
            {
                toggle.isOn = true;
            }
            else
            {
                toggle.isOn = false;
            }
        }
        toggleEvent();
    }

    public void toggleEvent()
    {
        if (toggle.isOn)
        {
            image.color = new Color(255, 255, 255, 255);
            if (this.gameObject.name == "Keyboard")
            {
                appSettings.isUsingKeyboard = true;
            }
            else
            {
                appSettings.isUsingKeyboard = false;
            }
        }
        else
        {
            image.color = new Color(255, 255, 255, 0);
        }
    }
}
