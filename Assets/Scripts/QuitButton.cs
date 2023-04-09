using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private FacePanel facePanel;
    [SerializeField] private UIPanelController uiPanelController;
    [SerializeField] private MoodBackground moodBackground;

    private void Awake()
    {
        uiPanelController = FindObjectOfType<UIPanelController>();
    }

    private void OnMouseDown()
    {
        uiPanelController.QuitApp();
    }

    private void OnMouseEnter()
    {
        facePanel.ChangeSadMood();
        moodBackground.ChangeSadMood();
    }

    private void OnMouseExit()
    {
        facePanel.ChangeHappyMood();
        moodBackground.ChangeHappyMood();
    }
}