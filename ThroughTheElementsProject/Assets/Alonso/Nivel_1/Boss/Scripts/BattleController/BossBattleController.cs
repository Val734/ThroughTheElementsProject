using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    public bool PRUEBA;
    public LiquidBoss_Behaviour boss;

    private void Awake()
    {
        boss = GetComponentInParent<LiquidBoss_Behaviour>();
    }

    private void OnValidate()
    {
        if (boss != null) 
        {
            boss.battleStarted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Contains("Player"))
        {
            Debug.Log("El Jugador Ha pasado por aqui");
            boss.battleStarted = true;
        }
    }
}
