using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadDisplay : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] private Text headName;
    [Header("Head Model")]
    [SerializeField] private GameObject headModel;

    public void UpdateHead(Head _newHead)
    {
        headName.text = _newHead.headName;
        if (headModel.transform.childCount > 0)
        {
            GameObject.Destroy(headModel.transform.GetChild(0).gameObject);
        }
        GameObject.Instantiate(_newHead.headModel, _newHead.headModel.transform.position, headModel.transform.rotation, headModel.transform);
    }
}
 