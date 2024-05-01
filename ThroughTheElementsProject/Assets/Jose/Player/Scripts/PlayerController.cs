using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using Cinemachine;
using TMPro;

using System;

public class PlayerController : MonoBehaviour
{
    public Healthbar StaminaBar;
    [SerializeField] public float stamina;
    [SerializeField] public float maxStamina=50;
   // [SerializeField] ParticleSystem frost;

    public CharacterController characterController;
    private Vector3 velocity = Vector3.zero;

    private bool isEnemyLocked;
    public bool playerIsHitted; 

    public UnityEvent OnJump;

    [Header("Detection")]
    public GameObject LockSphere;
    [SerializeField] public float detectionDistance = 5f;
    [SerializeField] LayerMask detectionLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] List<string> detectionTags = new List<string> { "Enemy" };

    [Header("Input Actions")]

    [SerializeField] InputActionReference Move;
    [SerializeField] InputActionReference Jump;
    [SerializeField] InputActionReference Run;
    [SerializeField] InputActionReference Lock;
    [SerializeField] InputActionReference ChangeTarget;
    [SerializeField] InputActionReference Dash;
    [SerializeField] InputActionReference BaseAttack;

    public Camera mainCamera;
    public float verticalVelocity = 0f;

    [Header("movement Settings")]
    [SerializeField] public float movementSpeed = 4f; //m/s al cuadrado
    [SerializeField] public float runMovementSpeed = 5f; //m/s al cuadrado
    [SerializeField] public float dashingSpeed = 16;

    [Header("Jump Settings")]

    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float angularSpeed = 360f;
    public bool playerCanAttack=true;

    public float gravity=-9.81f;
    public bool canJump=true;

    private Animator animator;
    public bool canMove;
    private bool isAttacking;

    [SerializeField] float flySpeed;
    private bool beingPushed;
    private Vector3 pushDirection;
    private bool isFreezed;

    float timer = 4f;
    float velocityToApply=0;


    public enum OrientationMode
    {
        CameraForward,
        ToTarget,
        ToMovement,
    }
    public OrientationMode orientationMode = OrientationMode.CameraForward;
    public GameObject LockCamera;
    [SerializeField] GameObject target;
    [SerializeField] GameObject lastTarget;

    public bool isDashing;
    private Vector2 Dashdirection;


    private bool[] comboAttack=new bool[3] { false, false, false };
    public GameObject AttackHit;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        //mainCamera = Camera.main;
        isEnemyLocked=false;
        isDashing=false;
        stamina = maxStamina;
        animator = GetComponentInChildren<Animator>();
        canMove = true;
        isFreezed = false;
        playerIsHitted= false;

    }
    private void OnEnable()
    {
        Move.action.Enable();
        Jump.action.Enable();
        Run.action.Enable();
        Lock.action.Enable();
        ChangeTarget.action.Enable();
        Dash.action.Enable();
        BaseAttack.action.Enable();
        //h
    }
    private void OnDisable()
    {
        Move.action.Disable();
        Jump.action.Disable();
        Run.action.Disable();
        Lock.action.Disable();
        ChangeTarget.action.Disable();
        Dash.action.Disable();

    }
    private float attackTime;
    private void Update()
    {
        //Debug.Log(playerIsHitted);
        StaminaBar.UpdateHealthbar(maxStamina, stamina);

        UpdateMovement();
        UpdateVerticalVelocity();
        UpdateOrientation();
        UpdateAttack();

        if (Lock.action.WasPressedThisFrame())
        {
            SelectTarget();
        }
        if(ChangeTarget.action.WasPressedThisFrame())
        {
            if (isEnemyLocked)
            {

                lastTarget = target;
                target = null;
                orientationMode = OrientationMode.CameraForward;
                BlockTarget();


            }
        }
        
        if (Dash.action.ReadValue<float>() > 0 ) 
        {
            StartCoroutine(DashOrRun());
            
        }
        else
        {
            RestoreStamina();
        }
        if(BaseAttack.action.triggered)
        {
            BeginBaseAttack();
        }

        if(isFreezed)
        {
            UpdateFreeze();
            velocityToApply = 0f;
            canMove = false;
            canJump = false;
            playerCanAttack = false;
            playerIsHitted= false;

            animator.SetBool("Frozen", true);
            animator.SetBool("HurtBool", false);

        }
        else if(!isFreezed && !playerIsHitted)
        {
            canMove = true;
            canJump=true;
            playerCanAttack=true;

            animator.SetBool("Frozen", false);


        }

    }

    

    Vector3 currentVelocity = Vector3.zero;
    float movementSmoothingSpeed = 5f;
    Vector3 smoothedMoveXZLocal = Vector3.zero;
    private void UpdateMovement()
    {
 
        if(canMove)
        {
            Vector2 rawMove;
            if (isDashing)
            {
                rawMove = Dashdirection;
            }
            else
            {
                rawMove = Move.action.ReadValue<Vector2>();
            }


            //Vector3 moveXZ= Vector3.forward * rawMove.y+
            //                Vector3.right*rawMove.x;

            Vector3 forward;
            forward = Camera.main.transform.forward;
            forward = Vector3.ProjectOnPlane(forward, Vector3.up).normalized;

            Vector3 right = Camera.main.transform.right;
            right = Vector3.ProjectOnPlane(right, Vector3.up).normalized;

            Vector2 direction = Move.action.ReadValue<Vector2>();
            Vector3 movement = (direction.y * forward) + (direction.x * right);

            Vector3 moveXZ = forward * rawMove.y +
                              right * rawMove.x;
            float velocityToApply = 0;

            if (!beingPushed)
            {
                if (!isFreezed && !isDashing)
                {
                    velocityToApply = Run.action.IsPressed() ? runMovementSpeed : movementSpeed;

                }
                else if (isDashing)
                {
                    velocityToApply = dashingSpeed;
                }

                currentVelocity = moveXZ * velocityToApply;
                velocity = currentVelocity;
            }
            else
            {
                currentVelocity = pushDirection * flySpeed;
            }

            characterController.Move(currentVelocity * Time.deltaTime);

            Vector3 moveXZLocal = transform.InverseTransformDirection(moveXZ);
            Vector3 currentMoveXZLocal = transform.InverseTransformDirection(moveXZ);


            Vector3 smoothingDirection = moveXZLocal - smoothedMoveXZLocal;

            float smoothingToApply = movementSmoothingSpeed * Time.deltaTime;
            smoothingToApply = Mathf.Min(smoothingToApply,
            smoothingDirection.magnitude);
            smoothedMoveXZLocal += smoothingDirection.normalized * smoothingToApply;

            float runningMultiplier = Dash.action.IsPressed() ? 2 : 1;

            animator.SetFloat("ForwardVelocity", smoothedMoveXZLocal.z * runningMultiplier);
            animator.SetFloat("SideWardVelocity", smoothedMoveXZLocal.x * runningMultiplier);



            //animator.SetBool("IsGrounded", characterController.isGrounded);
            //
        }

            
        
    }

    private void UpdateFreeze()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isFreezed = false;
            timer = 3f;
        }
    }
    public void BeingPushed(Vector3 windRayDirection, float pushForce)
    {
        beingPushed = true;
        pushDirection = windRayDirection;
        flySpeed = pushForce;
    }
    public void StopBeingPushed()
    {
        beingPushed = false;
    }
    public void StartFreezing()
    {
        isFreezed = true;
        Debug.Log(isFreezed);
    }
    public void StopFreezing()
    {
        isFreezed = false;
    }
    public bool IsFreezed()
    {
        return isFreezed;
    }


    private void UpdateVerticalVelocity()
    {

        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity *
        Time.deltaTime);
        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;
            animator.SetBool("IsGrounded", characterController.isGrounded);
        }
        if (Jump.action.WasPerformedThisFrame() &&
        characterController.isGrounded && canJump)
        {
            OnJump.Invoke();
            verticalVelocity = jumpSpeed;
            animator.SetBool("IsGrounded", !characterController.isGrounded);
        }
        if (!characterController.isGrounded)
        {
            animator.SetFloat("JumpProgress",
                Mathf.InverseLerp(jumpSpeed, -jumpSpeed, verticalVelocity)
                );
        }
    }

    void UpdateOrientation()
    {
        Vector3 desiredOrientation = Vector3.forward;

        switch (orientationMode)
        {
            case OrientationMode.CameraForward:
                desiredOrientation = mainCamera.transform.forward;
                LockCamera.SetActive(false);
                //freeLoockCamera.Follow = gameObject.transform;
                break;
            case OrientationMode.ToTarget:
                desiredOrientation = target.transform.position - transform.position;
                LockCamera.SetActive(true);

                break;
            case OrientationMode.ToMovement:
                desiredOrientation = currentVelocity;
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

    public void SelectTarget()
    {
        lastTarget = null;
        if(isEnemyLocked)
        {
            target = null;
            orientationMode = OrientationMode.CameraForward;
            isEnemyLocked = false;


        }
        else
        {

            BlockTarget();
        }
        
    }

    public void BlockTarget()
    {
        List<GameObject> targets = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(LockSphere.transform.position, detectionDistance, detectionLayerMask);
        for (int i = 0; i < colliders.Length && target == null; i++)
        {
            if (detectionTags.Contains(colliders[i].tag))
            {
                targets.Add(colliders[i].gameObject);
            }
        }


        float closestEnemyDistance = 100f;
        GameObject potentialTargetLock = null;
        Vector3 myPositionXZ = transform.position;
        myPositionXZ.y = 0f;
        for (int i = 0; i < targets.Count; i++)
        {
            float distance = Vector3.Distance(myPositionXZ, targets[i].transform.position);

            if (distance < closestEnemyDistance)
            {
                if(lastTarget!=null && lastTarget!= targets[i])
                {
                    closestEnemyDistance = distance;
                    potentialTargetLock = targets[i];
                    i = 110;
                }
                else
                {
                    closestEnemyDistance = distance;
                    potentialTargetLock = targets[i];
                }
                
            }
        }
        Debug.Log("Nombre del objeto " + potentialTargetLock.name);
        target = potentialTargetLock;
        orientationMode = OrientationMode.ToTarget;
        isEnemyLocked = true;
    }

    public void DashMovement()
    {
        stamina -= 20f;
        comboAttack[0] = false;
        comboAttack[1] = false;
        comboAttack[2] = false;

        
        StartCoroutine(StopDash());

    }
    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(0.25f);
        isDashing = false;
        gameObject.GetComponent<HealthBehaviour>().invulnerable = false;


    }
    private void RestoreStamina()
    {
        if (stamina < maxStamina)
        {
            stamina += 30f * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }
    }
    private void LooseStamina()
    {
        if (stamina > 0)
        {
            stamina -= 3f * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }
    }

    IEnumerator DashOrRun()
    {
        if (!isDashing && stamina > 10 && Dash.action.triggered )
        {
            Dashdirection = Move.action.ReadValue<Vector2>();
            Debug.Log(Dashdirection);
            
            if (Dashdirection == new Vector2(0, 0) )
            {
                Dashdirection = new Vector2(0, -1);
            }
            else if (Dashdirection.x < 0)
            {

                animator.SetTrigger("EvadeLeftTrigger");

            }
            else if (Dashdirection.x >0)
            {

                animator.SetTrigger("EvadeRightTrigger");

            }
            DashMovement();
            isDashing = true;
            RestoreAttackAnimation();
            gameObject.GetComponent<HealthBehaviour>().invulnerable = true;

        }
        yield return new WaitForSeconds(0.2f);
        
        if (Dash.action.ReadValue<float>() > 0 && !isDashing && stamina > 0)
        {
            Vector2 rawMove = Move.action.ReadValue<Vector2>();
            LooseStamina();
        }
    }

    public void BeginBaseAttack()
    {
        if (playerCanAttack)
        {
            if (!comboAttack[0])
            {
                isAttacking = true;
                comboAttack[0] = true;


            }
            else if (comboAttack[0] && !comboAttack[1])
            {
                comboAttack[1] = true;


            }
            else if (comboAttack[1] && !comboAttack[2])
            {
                comboAttack[2] = true;


            }
        }

    }
    private void UpdateAttack()
    {
        if (playerCanAttack)
        {
            if (comboAttack[0] && !isDashing)
            {
                attackTime += Time.deltaTime;
                if (attackTime > 0f && attackTime < 0.4f && !playerIsHitted && characterController.isGrounded && !isDashing)
                {

                    canMove = false;
                    animator.SetBool("AttackBool", true);
                    AttackHit.gameObject.SetActive(true);
                }
                else if (attackTime > 0.4f && attackTime < 0.8f && !playerIsHitted && characterController.isGrounded && !isDashing)
                {
                    AttackHit.gameObject.SetActive(false);
                }
                else if (attackTime > 0.8f && attackTime < 1f && comboAttack[1] && !playerIsHitted && characterController.isGrounded && !isDashing)
                {
                    AttackHit.gameObject.SetActive(true);

                }
                else if (attackTime > 1f && attackTime < 1.3f && comboAttack[1] && !playerIsHitted && characterController.isGrounded && !isDashing)
                {
                    animator.SetBool("AttackBool", true);
                    AttackHit.gameObject.SetActive(false);
                }
                else if (attackTime > 1.3f && attackTime < 2f && comboAttack[1] && comboAttack[2] && !playerIsHitted && characterController.isGrounded && !isDashing)
                {
                    AttackHit.gameObject.SetActive(true);
                    animator.SetBool("AttackBool", true);
                }
                else if (attackTime > 2f && comboAttack[1] && comboAttack[2] && !playerIsHitted && characterController.isGrounded && !isDashing)
                {
                    animator.SetBool("AttackBool", false);
                    AttackHit.gameObject.SetActive(false);
                    comboAttack[0] = false;
                    comboAttack[1] = false;
                    comboAttack[2] = false;
                    canMove = true;
                    isAttacking = false;
                }
                else
                {
                    RestoreAttackAnimation();
                }
            }
        }
    }

    private void RestoreAttackAnimation()
    {
        animator.SetBool("AttackBool", false);
        attackTime = 0f;
        AttackHit.gameObject.SetActive(false);
        comboAttack[0] = false;
        comboAttack[1] = false;
        comboAttack[2] = false;
        canMove = true;
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(LockSphere.transform.position, detectionDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(LockSphere.transform.position, LockSphere.transform.position + Vector3.up * 3f);
    }

    public void GetHurt()
    {
        playerIsHitted = true; 

        if (playerIsHitted)
        {
            animator.SetBool("HurtBool", true); 
            //animator.SetTrigger("HurtTrigger");
            isDashing = true;
            Dashdirection = Dashdirection = new Vector2(0, -1);
            canJump = false;

            playerCanAttack = false;

            StartCoroutine(stopHurt());


        }

    }
    IEnumerator stopHurt() 
    {

        
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        canMove = false;
        movementSpeed = 0f;
        yield return new WaitForSeconds(1.5f);
        canMove = true;
        canJump= true;
        playerCanAttack = true;
        playerIsHitted= false;

        movementSpeed = 4f;
        animator.SetBool("HurtBool", false);


    }
    public void GetDie()
    {
        if(!isFreezed)
        {
            animator.SetBool("HurtBool", true);

           // animator.SetTrigger("HurtTrigger");

        }
    }
    public void RestoreCamera()
    {
        lastTarget = null;
        if (isEnemyLocked)
        {
            target = null;
            orientationMode = OrientationMode.CameraForward;
            isEnemyLocked = false;


        }
    }

    public void RestorePlayer()
    {
        target = null;
        orientationMode = OrientationMode.CameraForward;
        isEnemyLocked = false;
        isDashing = false;
        stamina = maxStamina;
        canMove = true;
        isFreezed = false;
        playerIsHitted = false;
        canJump = true;
        playerCanAttack = true;
        movementSpeed = 4f;
        animator.SetBool("HurtBool", false);
    }
}
