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
    bool alive = true;

    [SerializeField] public float detectionDistance = 5f;
    [SerializeField] LayerMask detectionLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] List<string> detectionTags = new List<string> { "Player" };

    public Transform visualsPosition;
    private GameObject Player;

    private bool isAttacking;

    Animator animator;
    public AudioSource bubbleAttack;
    public AudioSource bubbleEnemyDead;

    protected override void ChildAwake()
    {
        isAttacking = false;
        animator=GetComponentInChildren<Animator>();
    }

    public void Dead()
    {
        state = State.Dead;
        alive = false;
        animator.SetTrigger("Dead"); //ARREGLAR LA POSICION DE LA ANIMACION
        Vector3 newPosition = visualsPosition.position;
        newPosition.y = newPosition.y - 0.86f;
        visualsPosition.position = newPosition;
        bubbleEnemyDead.Play();
    }

    protected override void ChildUpdate()
    {
        if(alive)
        {
            CreateOverlap();
        }
    }

    private void CreateOverlap()
    {
        Collider[] colliders2 = Physics.OverlapSphere(transform.position, detectionDistance, detectionLayerMask);
        for (int i = 0; i < colliders2.Length; i++)
        {
            if (colliders2[i].CompareTag("Player") && !isAttacking)
            {
                Player = colliders2[i].gameObject;
                animator.SetTrigger("AttackTrigger");
                //StartCoroutine(waitToAttack());
                isAttacking = true;
            }
        }
    }

    protected override float ReturnSpeed()
    {
        return localSpeed;
    }

    public void BubbleAttack()
    {
        bubbleAttack.Play();
        GameObject projectile = Instantiate(projectilePrefab, transform.forward + gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<Bubble>().Throw(transform.forward, transform.up);
        localSpeed = 2;
        isAttacking = false;
    }

}
