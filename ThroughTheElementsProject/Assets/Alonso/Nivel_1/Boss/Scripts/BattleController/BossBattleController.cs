using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    public LiquidBoss_Behaviour boss;
    public GameObject healthbar;

    bool musicStarted;
    public AudioSource BossBattleMusic;

    List<string> allTags = new List<string> {"Player"};


    public void ChangeToStateWaiting()
    {
        boss.state = LiquidBoss_Behaviour.StatesType.WaitingBattle;
        boss.playerOnArea = false;
        healthbar.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(allTags.Contains(other.tag)) 
        {
            ActivateBattle();
        }
    }

    public void ActivateBattle()
    {
        if (!musicStarted)
        {
            BossBattleMusic.Play();
            //musicStarted = true;
        }
        boss.StartBattle();
        healthbar.SetActive(true);
    }
}
