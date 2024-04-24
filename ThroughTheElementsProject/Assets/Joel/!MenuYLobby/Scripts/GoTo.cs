using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoTo : MonoBehaviour
{
    public string scene;
   public void GoToScene()
   {

        LevelLoader.OnLoadLevel(scene);
            
   }
}
