using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursory : MonoBehaviour
{
    private SpriteRenderer rend;

    private void Start()
    {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
