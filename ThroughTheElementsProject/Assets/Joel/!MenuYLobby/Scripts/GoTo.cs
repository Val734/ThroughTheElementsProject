using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoTo : MonoBehaviour
{
    /*Este script es el GoTo basicamente aqui haceis una funcion para la escena que querais ir*/
    [SerializeField] InputActionReference menu;
    private void OnEnable()
    {
        menu.action.Enable();
    }
    private void OnDisable()
    {
        menu.action.Disable();
    }
    public void GoToGameplay()
    {
        LevelLoader.OnLoadLevel("MainMenu");
    }
    public void GoToSettings()
    {
        LevelLoader.OnLoadLevel("Settings");
    }

    public void GoToHowToPlay()
    {
        LevelLoader.OnLoadLevel("HowToPlay");
    }
    public void GoToCredits()
    {
        LevelLoader.OnLoadLevel("Credits");
    }
    public static void GoToLevel1()
    {
        LevelLoader2.OnLoadLevel("PRUEBA");
    }
    public static void GoToLevel2()
    {
        LevelLoader2.OnLoadLevel("PRUEBA");
    }
    public static void GoToLevel3()
    {
        LevelLoader2.OnLoadLevel("PRUEBA");
    }
    public static void GoToLevel4()
    {
        LevelLoader2.OnLoadLevel("PRUEBA");
    }

    private void Update()
    {
        if (menu.action.triggered == true) { GoToGameplay(); }

    }
}
