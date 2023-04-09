using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBackground : MonoBehaviour
{
    private Renderer renderer;
    private Vector2 movement;
    public Vector2 speed;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        movement += speed * Time.deltaTime;
        renderer.material.mainTextureOffset = movement;
    }
}