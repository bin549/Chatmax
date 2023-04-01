using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayStartToSettingsAnimation()
    {
        animator.Play("StartToSettings");
    }
    
    
    public void PlaySettingsToStartAnimation()
    {
        animator.Play("SettingsToStart");
    }
    
    public void PlayStartToQuitAnimation()
    {
        animator.Play("StartToQuit");
    }

    public void PlayQuitToStartAnimation()
    {
        animator.Play("QuitToStart");
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
