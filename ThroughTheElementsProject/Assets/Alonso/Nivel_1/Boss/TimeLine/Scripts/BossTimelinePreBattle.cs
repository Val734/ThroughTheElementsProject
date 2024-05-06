using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BossTimelinePreBattle : MonoBehaviour
{
    public PlayableDirector director;
    public Transform safePlace;

    [Header("Audio Settings")]
    public AudioSource musicPreBattle;
    public AudioSource MainMusic;
    public AudioSource bossRoarSound;

    public LiquidBoss_Behaviour boss;
    bool reproduced;
    Transform Player;

    private void Awake()
    {
        director.stopped += OnStopCinematic;
        reproduced = false;
    }
    private void OnStopCinematic(PlayableDirector director)
    {
        Player.gameObject.SetActive(true);
        Player.position = safePlace.position;
        Player.GetComponent<PlayerController>().isDashing = false;
        director.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(director && !reproduced)
            {
                boss.GetComponent<Animator>().SetTrigger("WaveAttack");
                StartCoroutine(RoarSound());
                MainMusic.Stop();
                Player = other.transform;
                musicPreBattle.Play();
                director.gameObject.SetActive(true);
                director.Play();
                reproduced = true;
                Player.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator RoarSound()
    {
        yield return new WaitForSeconds(2);
        bossRoarSound.Play();
    }
}
