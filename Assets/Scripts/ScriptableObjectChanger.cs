using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;
    [SerializeField] private ScriptableObject[] scriptableObjects_zh;
    [SerializeField] private ScriptableObject[] scriptableObjects_en;
    [SerializeField] private HeadDisplay headDisplay;
    public int currentHeadIndex = 0;
    [SerializeField] private HeadContainer headContainer;
    private AppSettings appSettings;

    private void Awake()
    {
        appSettings = FindObjectOfType<AppSettings>();
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
            headDisplay.UpdateHead((Head)scriptableObjects[_index]);
            appSettings.avatar = ((Head)scriptableObjects[_index]).headAvatar;
            headContainer.UpdateHead();
        }
    }
}