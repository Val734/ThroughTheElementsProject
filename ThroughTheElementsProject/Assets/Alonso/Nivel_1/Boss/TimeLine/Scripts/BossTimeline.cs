using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BossTimeline : MonoBehaviour
{
    public PlayableDirector director;

    private void Awake()
    {
        director.stopped += OnStopCinematic;
    }

    private void OnStopCinematic(PlayableDirector director)
    {
       director.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(director)
            {
                director.gameObject.SetActive(true);
                director.Play();
            }
        }
    }
}
