using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayLevel : MonoBehaviour
{
    [SerializeField] InputActionReference menu;
    private void OnEnable()
    {
        menu.action.Enable();
    }
    private void OnDisable()
    {
        menu.action.Disable();
    }

    // Start is called before the first frame update

    // Update is called once per frame

    private void Update()
    {
        if (menu.action.triggered == true) {GoTo.GoToGameplay(); }

    }
}
