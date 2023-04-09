using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] private bool isSelectScene;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isSelectScene)
        {
            cursorPos += new Vector2(0, -2.3f);
        }
        transform.position = cursorPos;
        transform.position = new Vector3(transform.position.x, transform.position.y, -8f);
    }
}