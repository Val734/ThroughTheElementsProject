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
    GameObject player;

    float localSpeed = 2f;
    private float disappear = 5f;
    private float timeBetweenAttacks = 2f; // Tiempo entre ataques en segundos
    private float attackTimer = 0f; // Temporizador para controlar el tiempo entre ataques

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
        if (isAlive && isPlayerInRange)
        {
            if (attackTimer <= 0f)
            {
                // Atacar si el temporizador ha alcanzado cero
                animator.SetBool("Attack", true);
                localSpeed = 0;
                hitCollider.gameObject.SetActive(true);
                // Restablecer el temporizador de ataque
                attackTimer = timeBetweenAttacks;
            }
            else
            {
                // Contar hacia abajo el temporizador de ataque
                attackTimer -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("Attack", false);
            localSpeed = 2;
            hitCollider.gameObject.SetActive(false);
        }

        if (!isAlive)
        {
            disappear -= Time.deltaTime;
            if (disappear < 0)
            {
                Debug.Log("activar particulas para que la palme y se vaya alv");
                gameObject.SetActive(false);
            }
        }
        if (isAlive == false)
        {
            state = State.Dead;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
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
