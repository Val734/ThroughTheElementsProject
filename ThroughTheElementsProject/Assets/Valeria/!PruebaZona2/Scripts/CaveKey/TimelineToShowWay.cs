using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineToShowWay : MonoBehaviour
{
    public PlayableDirector timeLine;
    bool reproduced = false;

    void Start()
    {
        timeLine.gameObject.SetActive(true);
        timeLine.stopped += StopTimeLine;
        timeLine.Play();
        reproduced = true;
    }

    private void StopTimeLine(PlayableDirector director)
    {
        if (reproduced) // Solo desactiva si la línea de tiempo ya ha sido reproducida
        {
            director.gameObject.SetActive(false);
        }
    }
}
