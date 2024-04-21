using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimationBehavior : MonoBehaviour
{
    [SerializeField] InputActionReference mouse;
    [SerializeField] Animator _ar;

    private void Update()
    {
        PlayAnimation();
    }

    private void OnEnable()
    {
        mouse.action.Enable();
    }
    private void OnDisable()
    {
        mouse.action.Disable(); 
    }
    void PlayAnimation()
    {
        if(mouse.action.triggered)
        {
            _ar.Play("walking");
        }
    }
}