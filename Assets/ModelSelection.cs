using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelection : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;

  
    private void Start()
    {
        transitionAnimator.SetTrigger("End");
    }
}
