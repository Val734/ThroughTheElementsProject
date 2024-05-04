using UnityEngine;
using System.Collections.Generic;

public class UnlockPortal : MonoBehaviour
{
    public GameObject activarPortal;
    [SerializeField] int totalEnemies;
    private int enemyCounter;


    void Update()
    {
        if (totalEnemies == enemyCounter)
        {
            activarPortal.SetActive(true);
        }
    }

    public void addEnemyDead()
    {
        enemyCounter++;

        Debug.Log("enemigoMorido");
    }


}