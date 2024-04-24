using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayLevel : MonoBehaviour
{
    [SerializeField] InputActionReference menu;
    [SerializeField] GoTo gt;
    private void OnEnable()
    {
        menu.action.Enable();
    }
    private void OnDisable()
    {
        menu.action.Disable();
    }

    private void Update()
    {
        if(menu.action.triggered) { gt.GoToScene(); }
    }
    // Start is called before the first frame update

    // Update is called once per frame

}
