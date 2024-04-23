using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public abstract class EnemyController : MonoBehaviour
{
    protected State state = State.Wandering;
    protected enum State
    {
        Wandering,
        Chasing,
        Dead
    }

    [Header("Vertical Movement Settings")]
    float gravity = -9.81f;
    float verticalSpeed = 0f;

    [Header("Orientation Settings")]
    public float angularSpeed = 360f;
    Vector3 currentVelocity;

    [Header("Wandering Settings")]
    [SerializeField] float maxWanderPosition;
    Vector3 wanderPosition;

    [Header("Chasing Settings")]
    [SerializeField] LayerMask layerMask;
    Collider[] objectsInRange;
    protected Transform target;
    protected bool playerFreezed;

    protected CharacterController characterController;
    Vector3 originalPosition;
    protected float speed = 2f;
    float minDistanceToCheckWander = 0.5f;
    private void Awake()
    {
        originalPosition = transform.position;
        characterController = GetComponent<CharacterController>();
        ChildAwake();
        ChooseWanderPosition();
    }
    private void Update()
    {
        ChildUpdate();
        switch (state)
        {
            case State.Wandering:
                if(characterController.enabled)
                { 
                    MoveToTarget(wanderPosition);
                    UpdateOrientation();
                    VerticalMovement();
                }
                

                if (Vector3.Distance(wanderPosition, transform.position) < minDistanceToCheckWander)
                {
                    ChooseWanderPosition();
                }
                DetectEnemy();
                if (target != null) { state = State.Chasing; }
                break;
            case State.Chasing:
                MoveToTarget(target.position);
                UpdateOrientation();
                VerticalMovement();

                DetectEnemy();
                if (target == null) { state = State.Wandering; }
                break;

            case State.Dead:
                speed = 0f;
                break;
        }
    }
    private void VerticalMovement()
    {
        verticalSpeed += gravity * Time.deltaTime;

        characterController.Move(Vector3.up * verticalSpeed * Time.deltaTime);

        if (characterController.isGrounded)
        {
            verticalSpeed = 0f;
        }
    }
    void UpdateOrientation()
    {
        Vector3 desiredDirection = currentVelocity;
        desiredDirection.y = 0f;

        float angularDistance = Vector3.SignedAngle(transform.forward, desiredDirection, Vector3.up);

        float angleBecauseSpeed = angularSpeed * Time.deltaTime;
        float remainingAngle = Mathf.Abs(angularDistance);
        float angleToApply = Mathf.Sign(angularDistance) * Mathf.Min(angleBecauseSpeed, remainingAngle);

        Quaternion rotationToApply = Quaternion.AngleAxis(angleToApply, Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;
    }
    private void MoveToTarget(Vector3 targetPosition)
    {
        currentVelocity = (targetPosition - transform.position).normalized;
        currentVelocity.y = 0f;
        characterController.Move(currentVelocity * ReturnSpeed() * Time.deltaTime);
    }
    private void ChooseWanderPosition()
    {
        Vector2 randomXY = Random.insideUnitCircle * maxWanderPosition;
        Vector3 randomXZ = new Vector3(randomXY.x, 0f, randomXY.y);

        wanderPosition = originalPosition + randomXZ;
    }
    private void DetectEnemy()
    {
        objectsInRange = Physics.OverlapSphere(transform.position, maxWanderPosition, layerMask);
        if (objectsInRange.Length > 0)
        {
            target = objectsInRange[0].gameObject.transform;
        }
        else
        {
            target = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originalPosition, maxWanderPosition);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wanderPosition, wanderPosition + Vector3.up * 3);
    }
    protected abstract void ChildUpdate();
    protected abstract void ChildAwake();
    protected abstract float ReturnSpeed();
}
