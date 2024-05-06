using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StoryIntro : MonoBehaviour
{
    public PlayableDirector timeLine;
    public GoTo changeScene;
    bool reproduced = false;

    private void Awake()
    {
        changeScene = gameObject.GetComponent<GoTo>();
    }
    void Start()
    {
        timeLine.gameObject.SetActive(true);
        timeLine.stopped += StopTimeLine;
        timeLine.Play();
        reproduced = true;
    }

    private void StopTimeLine(PlayableDirector director)
    {
        if (reproduced)
        {
            director.gameObject.SetActive(false);
            changeScene.GoToScene();
        }
    }

}