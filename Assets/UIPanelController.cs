using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanelController : MonoBehaviour
{
    private Animator UIAnimator;
    [SerializeField] private Animator transitionFaderAnimator;
    [SerializeField] private Animator transitionChatGPTWiperAnimator;
    [SerializeField] private Transform crossfade;
    public float transitionTime = 1f;

    private void Awake()
    {
        UIAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        transitionFaderAnimator.SetTrigger("End");
    }

    public void NextScene() {
        crossfade.gameObject.SetActive(true);
        transitionChatGPTWiperAnimator.SetTrigger("Start");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void PlayStartToSettingsAnimation()
    {
        UIAnimator.Play("StartToSettings");
    }
    
    
    public void PlaySettingsToStartAnimation()
    {
        UIAnimator.Play("SettingsToStart");
    }
    
    public void PlayStartToQuitAnimation()
    {
        UIAnimator.Play("StartToQuit");
    }

    public void PlayQuitToStartAnimation()
    {
        UIAnimator.Play("QuitToStart");
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
