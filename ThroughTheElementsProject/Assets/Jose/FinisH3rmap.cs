using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinisH3rmap : MonoBehaviour
{
    public string scene;

    private void Awake()
    {
        NavigateToNextScene();
    }
    public void NavigateToNextScene()
    {
        SceneManager.LoadScene(scene);
    }
}
