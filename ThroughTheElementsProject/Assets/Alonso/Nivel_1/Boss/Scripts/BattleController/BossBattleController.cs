using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    public LiquidBoss_Behaviour boss;
    public GameObject healthbar;

    bool musicStarted;
    public AudioSource BossBattleMusic;

    private void Awake()
    {
        boss = GetComponentInParent<LiquidBoss_Behaviour>();
    }

    public void ChangeToStateWaiting()
    {
        boss.state = LiquidBoss_Behaviour.StatesType.WaitingBattle;
        boss.playerOnArea = false;
        healthbar.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Contains("Player"))
        {
            if(!musicStarted)
            {
                BossBattleMusic.Play();
                musicStarted = true;
            }
            boss.battleStarted = true;
            boss.playerOnArea = true;
            healthbar.SetActive(true);
        }
    }
}
