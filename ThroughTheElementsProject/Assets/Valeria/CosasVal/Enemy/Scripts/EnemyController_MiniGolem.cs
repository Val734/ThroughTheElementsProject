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

    [Header("Sound Settings")]
    [SerializeField] GameObject soundManager;
    private AudioSource hurtSound;
    private AudioSource attackSound;
    private AudioSource idleSound;


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

        hurtSound = soundManager.transform.Find("MiniGolemInjured").GetComponent<AudioSource>();
        attackSound = soundManager.transform.Find("MiniGolemIdle").GetComponent<AudioSource>();

    }

    protected override void ChildUpdate()
    {
        playerDetected = false;
        Collider[] entitiesInRange = Physics.OverlapSphere(transform.position, 1);

        bool playerFound = false; 

        foreach (Collider entity in entitiesInRange)
        {
            if (entity.CompareTag("Player"))
            {
                playerFound = true; 
                isPlayerInRange = true;
                playerDetected = true;
                break; 
            }
        }

        if (!playerFound) 
        {
            isPlayerInRange = false;
            hitCollider.gameObject.SetActive(false);
            localSpeed = 2f; 
        }
        else 
        {
            if (isAlive && isPlayerInRange)
            {
                if (attackTimer <= 0f && !isAttacking)
                {
                    animator.SetTrigger("Attacking");
                    attackSound.Play();
                    localSpeed = 0;
                    isAttacking = true;
                    hitCollider.gameObject.SetActive(true);
                }

                if (attackTimer <= -0.5f && isAttacking)
                {
                    attackTimer = timeBetweenAttacks;
                    localSpeed = 0;
                    hitCollider.gameObject.SetActive(false);
                    isAttacking = false;
                    attackSound.Stop();

                }
                else
                {
                    attackTimer -= Time.deltaTime;
                }
            }
        }

        if (!isAlive)
        {
            disappear -= Time.deltaTime;
            state = State.Dead;
            if (disappear < 0)
            {
                // Debug.Log("activar particulas para que la palme y se vayaaaa");
                Destroy(gameObject);
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
        hurtSound.Play();
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        hitCollider.gameObject.SetActive(false);
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
