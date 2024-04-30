using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    public LiquidBoss_Behaviour boss;
    public GameObject healthbar;

    private void Awake()
    {
        boss = GetComponentInParent<LiquidBoss_Behaviour>();
    }

    public void ChangeToStateWaiting()
    {
        boss.state = LiquidBoss_Behaviour.StatesType.PlayerKilled;
        boss.playerOnArea = false;

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Contains("Player"))
        {
            boss.battleStarted = true;
            boss.playerOnArea = true;
            healthbar.SetActive(true);
        }
    }
}
