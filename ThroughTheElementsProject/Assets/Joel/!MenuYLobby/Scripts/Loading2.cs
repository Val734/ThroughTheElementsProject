using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string nextLevelToLoad = LevelLoader2.nextLevel;
        StartCoroutine(this.MakeTheLoad(nextLevelToLoad));
    }

    IEnumerator MakeTheLoad(string level)
    {
        yield return new WaitForSeconds(10f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        while (operation.isDone == false) { yield return null; }
    }

}