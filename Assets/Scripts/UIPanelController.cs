using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanelController : MonoBehaviour
{
    private Animator openingUIAnimator;
    [SerializeField] private Transform crossfade;
    [SerializeField] private Transform circleWipe;
    [SerializeField] private Transform chatGPTWiper;
    [SerializeField] private Animator faderAnimator;
    [SerializeField] private Animator circleAnimator;
    [SerializeField] private Animator chatGPTWiperAnimator;
    [SerializeField] private AppSettings appSettings;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private FacePanel facePanel;

    private MouseCursor mouseCursor;
    public float transitionTime = 1f;

    private void Awake()
    {
        this.appSettings = GameObject.FindObjectOfType<AppSettings>();
        this.audioManager = GameObject.FindObjectOfType<AudioManager>();
        this.mouseCursor = GameObject.FindObjectOfType<MouseCursor>();
        this.openingUIAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (this.appSettings.isBackFromSelectScene)
        {
            this.chatGPTWiper.gameObject.SetActive(true);
            this.chatGPTWiperAnimator.SetTrigger("End");
        }
        else if (this.appSettings.isBackFromChatScene)
        {
            this.circleWipe.gameObject.SetActive(true);
            this.circleAnimator.SetTrigger("End");
        }
        else
        {
            this.crossfade.gameObject.SetActive(true);
            this.faderAnimator.SetTrigger("End");
        }
    }

    public void NextScene()
    {
        this.mouseCursor.gameObject.SetActive(false);
        this.audioManager.PlayClickSound();
        Cursor.visible = true;
        this.chatGPTWiper.gameObject.SetActive(true);
        this.chatGPTWiperAnimator.SetTrigger("Start");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void PlayStartToSettingsAnimation()
    {
        this.audioManager.PlaySlipSound();
        this.openingUIAnimator.Play("StartToSettings");
    }

    public void PlaySettingsToStartAnimation()
    {
        this.audioManager.PlaySlipSound();
        this.openingUIAnimator.Play("SettingsToStart");
    }

    public void PlayStartToQuitAnimation()
    {
        this.audioManager.PlaySlipSound();
        this.openingUIAnimator.Play("StartToQuit");
        facePanel.ChangeHappyMood();
    }

    public void PlayQuitToStartAnimation()
    {
        this.audioManager.PlaySlipSound();
        this.openingUIAnimator.Play("QuitToStart");
    }

    public void QuitApp()
    {
        this.audioManager.PlayQuitSound();
        this.crossfade.gameObject.SetActive(true);
        this.faderAnimator.SetTrigger("Start");
    }
}
