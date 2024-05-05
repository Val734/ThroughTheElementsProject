using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Playables;
using System;

public class UnlockPortal : MonoBehaviour
{
    public GameObject activarPortal;
    [SerializeField] int totalEnemies;
    private int enemyCounter;

    public PlayableDirector timeLine;
    bool reproduced;

    private void Awake()
    {
        timeLine.gameObject.SetActive(false);
        timeLine.stopped += StopTimeLine;
    }

    private void StopTimeLine(PlayableDirector director)
    {
        director.gameObject.SetActive(false);
    }

    void Update()
    {
        if (totalEnemies == enemyCounter)
        {
            activarPortal.SetActive(true);

            if(!reproduced) 
            {
                timeLine.gameObject.SetActive(true);
                timeLine.Play();
                reproduced = true;
            }
        }
    }

    public void addEnemyDead()
    {
        enemyCounter++;

        Debug.Log("enemigoMorido");
    }


}