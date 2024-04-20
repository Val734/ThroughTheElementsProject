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

    float localSpeed = 2f;
    private float disappear = 5f;

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
            animator.SetBool("Attack", true);
            localSpeed = 0;
            hitCollider.gameObject.SetActive(true);
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
}
