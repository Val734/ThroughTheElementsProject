using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBossBehviour : MonoBehaviour
{
    public GameObject Boss;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Boss.GetComponent<BossBehaviour>().StopSpecialAttack();
            Debug.Log("Works");
        }
    }
}

