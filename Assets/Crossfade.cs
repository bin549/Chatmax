using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossfade : MonoBehaviour
{
    private Transform transform;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    public void Hide() 
    {
        transform.gameObject.SetActive(false);
    }
}
