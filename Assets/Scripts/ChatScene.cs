using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatScene : MonoBehaviour
{
    private AppSettings appSettings;
    [SerializeField] private GameObject chatAvatar;
    [SerializeField] private OVRLipSyncContextMorphTarget lipSyncContextMorphTarget;
    [SerializeField] private GameObject femaleAvatar;
    [SerializeField] private GameObject robotAvatar;


    private void Awake()
    {
        appSettings = FindObjectOfType<AppSettings>();
    }

    private void Start()
    {
        SetupAvatat();
    }

    private void SetupAvatat()
    {
        if (appSettings.isFemaleAvatar) {
            femaleAvatar.SetActive(true);
        } 
        else if (appSettings.isRobotAvatar) 
        {
            robotAvatar.SetActive(true);
            femaleAvatar.SetActive(false);
        }
        else
        {
            robotAvatar.SetActive(false);
            femaleAvatar.SetActive(false);
            chatAvatar = GameObject.Instantiate(appSettings.avatar);
        }
    }
}
