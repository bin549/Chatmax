using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatScene : MonoBehaviour
{
    private AppSettings appSettings;
    [SerializeField] private GameObject chatAvatar;

    private void Awake()
    {
        appSettings = FindObjectOfType<AppSettings>();
    }

    private void Start()
    {
        chatAvatar = Instantiate(appSettings.avatar);
    }
}
