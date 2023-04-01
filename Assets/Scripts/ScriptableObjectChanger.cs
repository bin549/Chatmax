using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;
    [SerializeField] private HeadDisplay headDisplay;
    public int currentHeadIndex = 0;
    [SerializeField] private HeadContainer headContainer;

    private void Awake()
    {
        ChooseHead(currentHeadIndex);
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
        if(headDisplay != null) {
            headDisplay.UpdateHead((Head)scriptableObjects[_index]);
            headContainer.UpdateHead();
        }
    }
} 