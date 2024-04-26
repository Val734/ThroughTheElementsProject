using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    public LiquidBoss_Behaviour boss;

    private void Awake()
    {
        boss = GetComponentInParent<LiquidBoss_Behaviour>();
    }

    public void ChangeToStateWaiting()
    {
        Debug.Log(" ----------------------------- LA PUTA FUNCION YA FUNCIONA -----------------------------");
        boss.state = LiquidBoss_Behaviour.StatesType.PlayerKilled;
        boss.playerOnArea = false;

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Contains("Player"))
        {
            Debug.Log("El Jugador Ha pasado por aqui");
            boss.battleStarted = true;
            boss.playerOnArea = true;
        }
    }
}
