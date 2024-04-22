using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Thrower_V2 : EnemyController
{
    [Header("Type of thrower")]
    [SerializeField] bool isBubbleThrower;

    [Header("Atacking Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform instantiatePosition;

    float localSpeed = 2f;

    protected override void ChildAwake()
    {
        
    }

    protected override void ChildUpdate()
    {
        
    }

    protected override float ReturnSpeed()
    {
        return localSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER DETECTADO");
            StartCoroutine(waitToAttack());
        }
    }

    IEnumerator waitToAttack()
    {
        Debug.Log("WAITEANDO AL ATTACK");
        localSpeed = 0;
        yield return new WaitForSeconds(1);
        GameObject projectile = Instantiate(projectilePrefab, instantiatePosition.position, Quaternion.identity);
        if(isBubbleThrower)
        {
            projectile.GetComponent<Bubble>().Throw(transform.forward, transform.up);
        }
        else
        {
            projectile.GetComponent<Iman>().Throw(transform.forward, transform.up);
        }
        yield return new WaitForSeconds(4);
        localSpeed = 2;
    }
}