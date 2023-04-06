using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeadDisplay : MonoBehaviour
{
    [Header("Description")] 
    [SerializeField] private TMP_Text headName;
    [Header("Head Model")] 
    [SerializeField] private GameObject headModel;

    public void UpdateHead(Head _newHead)
    {
        headName.text = _newHead.headName;
        if (headModel.transform.childCount > 0)
        {
            GameObject.Destroy(headModel.transform.GetChild(0).gameObject);
        }
        GameObject instance = GameObject.Instantiate(_newHead.headModel);
        GameObject head_obj = GameObject.Instantiate(_newHead.headModel, headModel.transform.position, headModel.transform.rotation, headModel.transform);
        head_obj.transform.localPosition = instance.transform.position;
        head_obj.transform.localRotation = instance.transform.rotation;
        Destroy(instance.gameObject);
    }
}
