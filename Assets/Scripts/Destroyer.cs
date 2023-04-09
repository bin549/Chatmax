using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void Start()
    {
        GameObject.Destroy(gameObject, .2f);
    }
}
