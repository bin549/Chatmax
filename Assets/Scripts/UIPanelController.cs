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
    public float transitionTime = 1f;
    [SerializeField] private AppSettings appSettings;
    
    private void Awake()
    {
        appSettings = GameObject.FindObjectOfType<AppSettings>();
        openingUIAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (appSettings.isBackFromSelectScene) {
            chatGPTWiper.gameObject.SetActive(true);
            chatGPTWiperAnimator.SetTrigger("End");
        } 
        else if (appSettings.isBackFromChatScene) {
            circleWipe.gameObject.SetActive(true);
            circleAnimator.SetTrigger("End");
        }
        else
        {
            crossfade.gameObject.SetActive(true);
            faderAnimator.SetTrigger("End");
        }
    }

    public void NextScene() {
        chatGPTWiper.gameObject.SetActive(true);
        chatGPTWiperAnimator.SetTrigger("Start");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void PlayStartToSettingsAnimation()
    {
        openingUIAnimator.Play("StartToSettings");
    }
    
    
    public void PlaySettingsToStartAnimation()
    {
        openingUIAnimator.Play("SettingsToStart");
    }
    
    public void PlayStartToQuitAnimation()
    {
        openingUIAnimator.Play("StartToQuit");
    }

    public void PlayQuitToStartAnimation()
    {
        openingUIAnimator.Play("QuitToStart");
    }

    public void QuitApp()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
