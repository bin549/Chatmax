using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUI : MonoBehaviour
{
    [SerializeField] private Animator headWipeAnimator;
    [SerializeField] private Animator circleWipeAnimator;
    [SerializeField] private Transform circleWipe;
    [SerializeField] private AppSettings appSettings;
    public float transitionTime = 1f;
    
    private void Awake()
    {
        appSettings = GameObject.FindObjectOfType<AppSettings>();
    }
    
    private void Start()
    {
        headWipeAnimator.SetTrigger("End");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            appSettings.isBackFromSelectScene = false;
            appSettings.isBackFromChatScene = true;
            circleWipe.gameObject.SetActive(true);
            circleWipeAnimator.SetTrigger("Start");
            StartCoroutine(LoadLevel(0));
        }
    }

    IEnumerator LoadLevel(int levelIndex) {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void ToMenu()
    {
        circleWipe.gameObject.SetActive(true);
        circleWipeAnimator.SetTrigger("Start");
    }
}
