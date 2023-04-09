using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodBackground : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material happyMaterial;
    [SerializeField] private Material sadMaterial;
    [SerializeField] private AnimatedBackground animatedBackground;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        animatedBackground = GetComponent<AnimatedBackground>();
    }

    public void ChangeSadMood()
    {
        renderer.material = sadMaterial;
        animatedBackground.speed = new Vector2(0, 3);
    }

    public void ChangeHappyMood()
    {
        renderer.material = happyMaterial;
        animatedBackground.speed = new Vector2(0, -1);
    }
}