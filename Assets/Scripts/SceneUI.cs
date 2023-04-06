using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        appSettings = GameObject.FindObjectOfType<AppSettings>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        qaController = GameObject.FindObjectOfType<QAController>();
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
            } else {
                qaController.Show();
            }
        }
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
        if (appSettings.isMale) {
            sexImage.sprite = maleSprite;
        } else {
            sexImage.sprite = femaleSprite;
        }
        if (appSettings.isUsingKeyboard) 
        {
            inputImage.sprite = keyboardSprite;
        }
        else
        {
            inputImage.sprite = microphoneSprite;
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
