using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneUI : MonoBehaviour
{
    [SerializeField] private Animator headWipeAnimator;
    [SerializeField] private Animator circleWipeAnimator;
    [SerializeField] private Transform circleWipe;
    [SerializeField] private AppSettings appSettings;
    [SerializeField] private GameObject UIComponents;
    public float transitionTime = 1f;
    public bool isUIActive = false;

    [SerializeField] private Image inputImage;
    [SerializeField] private Sprite microphoneSprite;
    [SerializeField] private Sprite keyboardSprite;
    [SerializeField] private Image sexImage;
    [SerializeField] private Sprite maleSprite;
    [SerializeField] private Sprite femaleSprite;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private QAController qaController;
    [SerializeField] private GameObject microphoneLogo;
    [SerializeField] private GameObject keyboardLogo;
    [SerializeField] private GameObject appVoiceExperienceObj;
    [SerializeField] private Meta.WitAi.TTS.Utilities.TTSSpeaker ttsSpeaker;
    public TMP_Text promptField;

    private void Awake()
    {
        appSettings = GameObject.FindObjectOfType<AppSettings>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        qaController = GameObject.FindObjectOfType<QAController>();
        ttsSpeaker = GameObject.FindObjectOfType<Meta.WitAi.TTS.Utilities.TTSSpeaker>();
    }

    private void Start()
    {
        headWipeAnimator.SetTrigger("End");
        InitUIComponent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isUIActive)
            {
                BackHome();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isUIActive = !isUIActive;
            UIComponents.SetActive(isUIActive);
            audioManager.PlayTapSound();
            if (isUIActive)
            {
                qaController.Clear();
                promptField.gameObject.SetActive(false);
                if (appSettings.isUsingKeyboard)
                {
                    keyboardLogo.SetActive(false);
                }
                else
                {
                    microphoneLogo.SetActive(false);
                    qaController.isSpeakng = false;
                }
            }
            else
            {
                qaController.isFirstSpeaking = true;
                qaController.isFirstQuestion = true;
                promptField.gameObject.SetActive(true);
                if (appSettings.isUsingKeyboard)
                {
                    keyboardLogo.SetActive(true);
                    promptField.text = "Try to write something.";
                }
                else
                {
                    microphoneLogo.SetActive(true);
                    promptField.text = "Try to say something.";
                    qaController.Show();
                };
            }
        }
    }

    public void ToggleInputDevice()
    {
        appSettings.isUsingKeyboard = !appSettings.isUsingKeyboard;
        inputImage.sprite = appSettings.isUsingKeyboard ? keyboardSprite : microphoneSprite;
    }

    public void BackHome()
    {
        audioManager.PlayHomeSound();
        appSettings.isBackFromSelectScene = false;
        appSettings.isBackFromChatScene = true;
        circleWipe.gameObject.SetActive(true);
        circleWipeAnimator.SetTrigger("Start");
        StartCoroutine(LoadLevel(0));
    }

    private void InitUIComponent()
    {
        if (appSettings.isMale)
        {
            sexImage.sprite = maleSprite;
            ttsSpeaker.presetVoiceID = "COOPER";
        }
        else
        {
            sexImage.sprite = femaleSprite;
            ttsSpeaker.presetVoiceID = "REBECCA";
        }
        if (appSettings.isUsingKeyboard)
        {
            inputImage.sprite = keyboardSprite;
            microphoneLogo.SetActive(false);
            keyboardLogo.SetActive(true);
            promptField.text = "Try to write something.";
        }
        else
        {
            //  appVoiceExperienceObj.SetActive(true);
            inputImage.sprite = microphoneSprite;
            microphoneLogo.SetActive(true);
            keyboardLogo.SetActive(false);
            promptField.text = "Try to say something.";
        }
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void ToMenu()
    {
        audioManager.PlayHomeSound();
        circleWipe.gameObject.SetActive(true);
        circleWipeAnimator.SetTrigger("Start");
    }
}