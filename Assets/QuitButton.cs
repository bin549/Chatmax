using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private FacePanel facePanel;
    [SerializeField] private UIPanelController uiPanelController;

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
    }

    private void OnMouseExit()
    {
        facePanel.ChangeHappyMood();
    }
}
