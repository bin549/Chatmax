using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelSelection : MonoBehaviour
{
    [SerializeField] private Transform headWiper;
    [SerializeField] private Transform chatGPTWiper;
    [SerializeField] private Animator chatGPTWiperAnimator;
    [SerializeField] private Animator headWiperAnimator;
    [SerializeField] private AppSettings appSettings;
    public float transitionTime = 1f;

    private void Awake()
    {
        appSettings = GameObject.FindObjectOfType<AppSettings>();
    }

    private void Start()
    {
        chatGPTWiper.gameObject.SetActive(true);
        chatGPTWiperAnimator.SetTrigger("End");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToMainScene();
        }
    }

    public void ToMainScene()
    {
        appSettings.isBackFromChatScene = false;
        appSettings.isBackFromSelectScene = true;
        chatGPTWiper.gameObject.SetActive(true);
        chatGPTWiperAnimator.SetTrigger("Start");
        StartCoroutine(LoadLevel(0));
    }

    public void ToChatScene()
    {
        headWiper.gameObject.SetActive(true);
        headWiperAnimator.SetTrigger("Start");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
