using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Thrower : EnemyController
{
    [Header("Type of thrower")]
    [SerializeField] bool isBubbleThrower;
    //[SerializeField] bool isRobotThrower;

    [Header("Atacking Settings")]
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float throwTime = 0.7f;

    float localSpeed = 2f;

    [SerializeField] public float detectionDistance = 5f;
    [SerializeField] LayerMask detectionLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] List<string> detectionTags = new List<string> { "Player" };

    private GameObject Player;

    private bool isAttacking;

    Animator animator;

    protected override void ChildAwake()
    {
        isAttacking = false;
        animator=GetComponentInChildren<Animator>();
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
        animator.SetTrigger("AttackTrigger");
        localSpeed = 0;
        yield return new WaitForSeconds(throwTime);
        GameObject projectile = Instantiate(projectilePrefab, transform.forward + gameObject.transform.position, Quaternion.identity);

        projectile.GetComponent<Bubble>().Throw(transform.forward, transform.up);

        localSpeed = 2;

        isAttacking = false;
    }
}
