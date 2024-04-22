using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoTo : MonoBehaviour
{
    /*Este script es el GoTo basicamente aqui haceis una funcion para la escena que querais ir*/
    public static void GoToGameplay()
    {
        Time.timeScale = 1f;
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
        LevelLoader2.OnLoadLevel("Nivel_1");
    }
    public static void GoToLevel2()
    {
        LevelLoader2.OnLoadLevel("Zona2P1");
    }
    public static void GoToLevel3()
    {
        LevelLoader2.OnLoadLevel("ThirdScene");
    }
    public static void GoToLevel4()
    {
        LevelLoader2.OnLoadLevel("BossFight");
    }
}
