using UnityEngine;

[CreateAssetMenu(fileName="Head", menuName="Scriptable Object/Head")]
public class Head : ScriptableObject
{
    [Header("Head Name")]
    public string headName;

    [Header("Head Model")]
    public GameObject headModel;
}
