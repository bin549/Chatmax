using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppSettings : MonoBehaviour
{
    public bool isBackFromSelectScene = false;
    public bool isBackFromChatScene = false;
    public bool isUsingKeyboard = false;
    public bool isFullScreen = true;
    
    public GameObject avatar;
    
    private static AppSettings instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void SetAvatar(GameObject avatar)
    {
        this.avatar = avatar;
    }

    public GameObject GetAvatar()
    {
        if (avatar != null)
        {
            return avatar;
        }
        return null;
    }

}
