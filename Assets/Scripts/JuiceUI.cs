using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceUI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        animator.SetTrigger("StartJuice");
    }

    private void OnMouseExit()
    {
        animator.SetTrigger("StopJuice");
    }
}
