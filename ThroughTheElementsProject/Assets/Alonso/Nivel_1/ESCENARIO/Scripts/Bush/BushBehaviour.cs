using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushBehaviour : MonoBehaviour
{
    BoxCollider boxCollider;
    Animator animator;
    bool hitted;

    private void Awake()
    {
        NotHittedYet();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void NotHittedYet()
    {
        hitted = false;
    }

    public void BushHitted()
    {
        if (!hitted) 
        {
            animator.SetBool("Dead", true);
            boxCollider.enabled = false;
            hitted = true;        
        }
    }
}
