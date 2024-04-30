using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoTo : MonoBehaviour
{
    public string scene;
   public void GoToScene()
   {
        Time.timeScale = 1.0f;
        LevelLoader.OnLoadLevel(scene);
            
   }
}
