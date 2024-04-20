using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        animator =  GetComponentInChildren<Animator>();
        canAttack = true;
        canCreateGravity = true;
    }
    private void Update()
    {
        UpdateOrientation();
        GravityToPlayer();
        DetectPlayer();
    }

    private void GravityToPlayer()
    {
        Collider[] colliders2 = Physics.OverlapSphere(Detector.transform.position, detectionDistance2, detectionLayerMask);
        for (int i = 0; i < colliders2.Length; i++)
        {
            if (colliders2[i].CompareTag("Player") && canCreateGravity)
            {
                if(canAttack==false)
                {
                    animator.SetTrigger("GravityTrigger");
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
                Player = colliders[i].gameObject;
                orientationMode=OrientationMode.ToTarget;
                Attack();
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
        if(canAttack)
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
        yield return new WaitForSeconds(2);            
        GameObject littleGravitySphere = Instantiate(GravitySphere, transform.position, Quaternion.identity);
        Vector3 direction = Player.transform.position - gameObject.transform.position;
        littleGravitySphere.GetComponent<GravitySphere>().ThrowBall(direction);
        canAttack=true;
    }
}
