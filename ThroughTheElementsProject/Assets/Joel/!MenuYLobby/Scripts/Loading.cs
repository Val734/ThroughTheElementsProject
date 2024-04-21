using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string nextLevelToLoad = LevelLoader.nextLevel;

        StartCoroutine(this.MakeTheLoad(nextLevelToLoad));
    }

    IEnumerator MakeTheLoad(string level)
    {
        yield return new WaitForSeconds(5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        while (operation.isDone == false) { yield return null; }
    }

}
