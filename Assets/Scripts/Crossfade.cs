using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossfade : MonoBehaviour
{
    private Transform transform;
    [SerializeField] private GameObject canvasToShow;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    public void Hide()
    {
        transform.gameObject.SetActive(false);
    }

    public void HidePro()
    {
        transform.gameObject.SetActive(false);
        canvasToShow.gameObject.SetActive(true);
    }
}
