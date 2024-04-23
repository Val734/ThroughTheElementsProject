using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Thrower : EnemyController
{
    [Header("Type of thrower")]
    [SerializeField] bool isBubbleThrower;
    [SerializeField] bool isRobotThrower;

    [Header("Atacking Settings")]
    [SerializeField] GameObject projectilePrefab;

    float localSpeed = 2f;

    [SerializeField] public float detectionDistance = 5f;
    [SerializeField] LayerMask detectionLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] List<string> detectionTags = new List<string> { "Player" };

    private GameObject Player;

    private bool isAttacking;


    protected override void ChildAwake()
    {
        
    }

    protected override void ChildUpdate()
    {
        CreateOverlap();
    }

    private void CreateOverlap()
    {

        Collider[] colliders2 = Physics.OverlapSphere(transform.position, detectionDistance, detectionLayerMask);
        for (int i = 0; i < colliders2.Length; i++)
        {
            if (colliders2[i].CompareTag("Player") && !isAttacking)
            {
                Player = colliders2[i].gameObject;
                StartCoroutine(waitToAttack());
                isAttacking = true;
        
            }
        }
    }

    protected override float ReturnSpeed()
    {
        return localSpeed;
    }

    
    IEnumerator waitToAttack()
    {
        

        localSpeed = 0;
        yield return new WaitForSeconds(3);
        GameObject projectile = Instantiate(projectilePrefab, transform.forward + gameObject.transform.position, Quaternion.identity);
        isAttacking = false;
        if (isBubbleThrower)
        {
            projectile.GetComponent<Bubble>().Throw(transform.forward, transform.up);
        }
        else if(isRobotThrower)
        {
            Vector3 direction = Player.transform.position - gameObject.transform.position;
            projectile.GetComponent<Rigidbody>().AddForce(direction*2.5f, ForceMode.VelocityChange);
        }
        else
        {
            projectile.GetComponent<Iman>().Throw(transform.forward, transform.up);
        }
        localSpeed = 2;
        isAttacking = false;
    }
}