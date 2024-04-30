using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Robot : EnemyController
{
    [Header("Type of thrower")]
    [SerializeField] bool isRobotThrower;

    [Header("Atacking Settings")]
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] float throwTime; // EL JOSE TIENE QUE PONER 3.5F

    float localSpeed = 2f;

    [SerializeField] public float detectionDistance = 5f;
    [SerializeField] LayerMask detectionLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] List<string> detectionTags = new List<string> { "Player" };

    private GameObject Player;

    private bool isAttacking;
    private bool canAttack;

    Animator animator;

    public GameObject Visuals;


    protected override void ChildAwake()
    {
        animator=GetComponentInChildren<Animator>();
        canAttack = true;
    }

    protected override void ChildUpdate()
    {
        CreateOverlap();
    }

    private void CreateOverlap()
    {
        if(canAttack)
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
        isAttacking = false;
        if(isRobotThrower && canAttack)
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

    public void DestroyedRobot()
    {
        state = State.Dead;
        canAttack = false;
        gameObject.GetComponent<HealthBehaviour>().healthbar = null;
        Visuals.GetComponent<Rigidbody>().isKinematic=false;
        Visuals.GetComponent<BoxCollider>().enabled=true;
        for (int i = 0; i < Visuals.transform.childCount; i++)
        {
            Transform child = Visuals.transform.GetChild(i);

            Collider childCollider = child.GetComponent<BoxCollider>();
            if (childCollider != null)
            {
                childCollider.enabled = true;
            }

            Rigidbody childRigidbody = child.GetComponent<Rigidbody>();
            if (childRigidbody != null)
            {
                childRigidbody.isKinematic = false;
            }
            StartCoroutine(FinishDestroy());
        }
    }

    IEnumerator FinishDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}