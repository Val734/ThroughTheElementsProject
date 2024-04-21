using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public static string nextLevel;

    public static void OnLoadLevel(string n)
    {
        nextLevel = n;

        SceneManager.LoadScene("Loading");
    }
}
