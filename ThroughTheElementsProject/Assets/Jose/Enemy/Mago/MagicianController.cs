using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static BossBehaviour;

public class MagicianController : MonoBehaviour
{
    [SerializeField] float angularSpeed = 360f;
    private GameObject Player;
    [SerializeField] public float detectionDistance = 5f;
    [SerializeField] public float detectionDistance2 = 10f;
    [SerializeField] LayerMask detectionLayerMask = Physics.DefaultRaycastLayers;

    public GameObject Detector;
    public GameObject GravitySphere;
    public GameObject BigGravitySphere;

    public GameObject Visuals;
    public GameObject MagicCircle;

    public bool isAlive;

    public UnityEvent onShoot;
    public UnityEvent onDie;
    public UnityEvent onCreateGravity;

    public enum OrientationMode
    {
        CameraForward,
        ToTarget,
        ToMovement,
    }
    public OrientationMode orientationMode = OrientationMode.CameraForward;

    Vector3 wanderPosition;
    float minDistanceToCheckWander = 0.5f;

    private Animator animator;

    private bool canAttack;
    private bool canCreateGravity;

    private void Awake()
    {
        isAlive = true;
        animator =  GetComponentInChildren<Animator>();
        canAttack = true;
        canCreateGravity = true;

        UnRagDoll();
    }
    private void Update()
    {
        if(isAlive)
        {
            UpdateOrientation();
            GravityToPlayer();
            DetectPlayer();
        }
        
    }

    private void GravityToPlayer()
    {
            Collider[] colliders2 = Physics.OverlapSphere(Detector.transform.position, detectionDistance2, detectionLayerMask);
            for (int i = 0; i < colliders2.Length; i++)
            {
                if (colliders2[i].CompareTag("Player") && canCreateGravity)
                {
                    if (canAttack == false && isAlive)
                    {
                        animator.SetTrigger("GravityTrigger");
                        onCreateGravity.Invoke();
                        GameObject bigGravitySphere = Instantiate(BigGravitySphere, colliders2[i].transform.position, Quaternion.identity);

                        canCreateGravity = false;
                        StartCoroutine(RestoreGravity());
                    }

                }
            }
        
        
    }

    private void DetectPlayer()
    {
            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, detectionDistance, detectionLayerMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Player"))
                {
                    if (isAlive)
                    {
                        Player = colliders[i].gameObject;
                        orientationMode = OrientationMode.ToTarget;
                        Attack();
                    }

                }
            }
        
        
        

    }

    private void UpdateOrientation()
    {
        Vector3 desiredOrientation = transform.forward;
        switch (orientationMode)
        {
            case OrientationMode.CameraForward:
                desiredOrientation = transform.forward;
                break;
            case OrientationMode.ToTarget:
                desiredOrientation = Player.transform.position - transform.position;
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

    private void Attack()
    {
        if(canAttack && isAlive)
        {
            
            StartCoroutine(RestartAttack());
            canAttack = false;
        }
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.position + Vector3.up * 3f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance2);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 3f);
    }

    IEnumerator RestoreGravity()
    {
        yield return new WaitForSeconds(8);
        canCreateGravity = true;
    }
    IEnumerator RestartAttack()
    {
        yield return new WaitForSeconds(1);
        animator.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(1.5f);
        onShoot.Invoke(); 
        yield return new WaitForSeconds(0.5f);  
        if(isAlive)
        {
            GameObject littleGravitySphere = Instantiate(GravitySphere, transform.position, Quaternion.identity);
            Vector3 direction = Player.transform.position - gameObject.transform.position;
            littleGravitySphere.GetComponent<GravitySphere>().ThrowBall(direction);
            canAttack = true;
        }
            
        
        
    }

    public void DestroyedMagician()
    {
        canAttack = false;
        orientationMode = OrientationMode.CameraForward;
        isAlive = false;
        Debug.Log(isAlive);
        MagicCircle.SetActive(false);
        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.GetComponent<HealthBehaviour>().healthbar = null;
        onDie.Invoke();
        Ragdollize();
    }
    private void UnRagDoll()
    {
        Collider[] coll = Visuals.GetComponentsInChildren<Collider>();
        Rigidbody[] rigid = Visuals.GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < coll.Length; i++)
        {
            coll[i].enabled = false;
            rigid[i].isKinematic = true;
        }

        animator.enabled = true;

    }
    private void Ragdollize()
    {
        //Activa collider y rigbody
        //Animator Disable
        Collider[] coll = Visuals.GetComponentsInChildren<Collider>();
        Rigidbody[] rigid = Visuals.GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < coll.Length; i++)
        {
            coll[i].enabled = true;
            rigid[i].isKinematic = false;
        }
        animator.enabled = false;

    }
}
