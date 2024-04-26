using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_MiniGolem : EnemyController
{
    [Header("Atacking Settings")]
    [SerializeField] GameObject hitCollider;
    private Animator animator;
    private bool isAlive;
    private bool isPlayerInRange;
    private bool playerDetected = false;
    GameObject player;

    float localSpeed = 2f;
    private float disappear = 5f;
    private float timeBetweenAttacks = 0.01f;
    private float attackTimer = 0f; 
    private bool isAttacking; 
    enum Look
    {
        normal, player
    }

    Look look = Look.normal;


    protected override void ChildAwake()
    {
        animator = GetComponentInChildren<Animator>();
        isAlive = true;
        isPlayerInRange = false;
    }

    protected override void ChildUpdate()
    {
        playerDetected = false;
        Collider[] entitiesInRange = Physics.OverlapSphere(transform.position,1);
        if(entitiesInRange.Length > 1)
        {
            foreach (Collider entitie in entitiesInRange)
            {
                if (entitie.CompareTag("Player"))
                {
                    isPlayerInRange = true;
                    playerDetected = true;
                }
            }
            if(!playerDetected && isPlayerInRange)
            {
                isPlayerInRange = false;
            }
        }
        else if(entitiesInRange.Length == 1)
        {
            if (entitiesInRange[0].CompareTag("Player"))
            {
                isPlayerInRange = true;
            }
            else if(isPlayerInRange)
            {
                isPlayerInRange = false;
            }
        }
        
        if (isAlive && isPlayerInRange)
        {
            if (attackTimer <= 0f && !isAttacking)
            {
                // Atacar si el temporizador ha alcanzado cero
                animator.SetTrigger("Attacking");
                localSpeed = 0;
                isAttacking = true;
                hitCollider.gameObject.SetActive(true);
            }

            if (attackTimer <= -0.5f && isAttacking)
            {
                Debug.Log("NO ME MUEVO");
                attackTimer = timeBetweenAttacks; 
                localSpeed = 0;
                hitCollider.gameObject.SetActive(false);
                isAttacking=false;

            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
        else if (!isPlayerInRange)
        {
            localSpeed = 2f;
        }
        else if (!isAlive)
        {
            disappear -= Time.deltaTime;
            state = State.Dead;
            if (disappear < 0)
            {
                Debug.Log("activar particulas para que la palme y se vaya alv");
                gameObject.SetActive(false);
            }
        }
        

        UpdateOrientation();

    }

    protected override float ReturnSpeed()
    {
        return localSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        isAlive = false;
        localSpeed = 0;
    }

    private void UpdateOrientation()
    {
        Vector3 desiredOrientation = Vector3.forward;

        switch (look)
        {
            case Look.normal:
                desiredOrientation = transform.forward;
                break;
            case Look.player:
                desiredOrientation = player.transform.position - transform.position;
                break;
        }

        desiredOrientation.y = 0f;
        float angularDistance =
            Vector3.SignedAngle(transform.forward, desiredOrientation,
        Vector3.up);

        float angleBecauseSpeed = angularSpeed * Time.deltaTime;
        float remainingAngle = Mathf.Abs(angularDistance);

        float angleToApply =
        Mathf.Sign(angularDistance) * Mathf.Min(angleBecauseSpeed,
        remainingAngle);


        Quaternion rotationToApply =
        Quaternion.AngleAxis(angleToApply, Vector3.up);

        transform.rotation = rotationToApply * transform.rotation;
    }
}
