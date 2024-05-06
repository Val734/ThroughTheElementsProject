using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossTimelinePostBattle : MonoBehaviour
{
    public GameObject Portal;
    public PlayableDirector director;
    public AudioSource WinMusic;

    private void Awake()
    {
        director.stopped += OnStopCinematic;
    }

    private void OnStopCinematic(PlayableDirector director)
    {
        Portal.SetActive(true);
        gameObject.SetActive(false);
    }
}
