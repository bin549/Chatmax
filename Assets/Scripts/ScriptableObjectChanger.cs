using UnityEngine;
using UnityEngine.UI;

public class ScriptableObjectChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;
    [SerializeField] private ScriptableObject[] scriptableObjects_zh;
    [SerializeField] private ScriptableObject[] scriptableObjects_en;
    [SerializeField] private HeadDisplay headDisplay;
    public int currentHeadIndex = 0;
    [SerializeField] private HeadContainer headContainer;
    private AppSettings appSettings;
    private AudioManager audioManager;
    [SerializeField] private Sprite maleSprite;
    [SerializeField] private Sprite femaleSprite;
    [SerializeField] private Image sexUI;

    private void Awake()
    {
        appSettings = FindObjectOfType<AppSettings>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        SetHeadSet();
        ChooseHead(currentHeadIndex);
    }

    private void SetHeadSet()
    {
        if (appSettings.isEnglish)
        {
            scriptableObjects = scriptableObjects_en;
        }
        else
        {
            scriptableObjects = scriptableObjects_zh;
        }
    }

    public void ChangeHead(int _index)
    {
        audioManager.PlayChangeSound();
        currentHeadIndex += _index;
        if (currentHeadIndex == -1)
        {
            currentHeadIndex = scriptableObjects.Length - 1;
        }
        if (currentHeadIndex == scriptableObjects.Length)
        {
            currentHeadIndex = 0;
        }
        ChooseHead(currentHeadIndex);
    }

    public void ChooseHead(int _index)
    {
        if (headDisplay != null)
        {
            Head head = (Head)scriptableObjects[_index];
            headDisplay.UpdateHead(head);
            appSettings.avatar = (head).headAvatar;
            if (head.isMale) 
            {
                sexUI.sprite = maleSprite;
            }
            else
            {
                sexUI.sprite = femaleSprite;
            }
            headContainer.UpdateHead();
        }
    }
}