using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BossTimeline : MonoBehaviour
{
    public PlayableDirector director;
    bool reproduced;
    Transform Player;
    public Transform safePlace;

    private void Awake()
    {
        director.stopped += OnStopCinematic;
        reproduced = false;
    }

    private void OnStopCinematic(PlayableDirector director)
    {
        Player.gameObject.SetActive(true);
        Player.position = safePlace.position;
        director.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(director && !reproduced)
            {
                Player = other.transform;
                director.gameObject.SetActive(true);
                director.Play();
                reproduced = true;
                Player.gameObject.SetActive(false);
            }
        }
    }
}
