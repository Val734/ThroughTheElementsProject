using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoTo : MonoBehaviour
{
   
   public static void GoToScene(string s)
    {

        LevelLoader.OnLoadLevel(s);
            
    }
}
