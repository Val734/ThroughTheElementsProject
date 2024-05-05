using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Este script cambia de fullscreen a windowed
public class Fullscreen : MonoBehaviour
{
    [SerializeField] Toggle tg;

    private void Start()
    {
        tg.isOn = false;
    }
    public void ChangeFullScreen()
    {
        if(tg.isOn)
        {
            Screen.fullScreen = false; //Cambia a ventana
        }
        else
        {
            Screen.fullScreen = true; //Cambia a fullscreen
        }
    }
}
