using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Events;


public class LiquidStateBehaviour : MonoBehaviour
{

    private PlayerController controller;
    private bool isOnLiquidState;
    private bool isOnUltraJumping;
    private bool canUltraJump=true;
    private bool isMovingTowardsObjective = false;
    private bool canTP = false;

    [SerializeField] InputActionReference TransformAction;
    [SerializeField] InputActionReference Hability;

    public GameObject Wave; 
    public GameObject Swirl;

    private Animator animator;

    public void Awake()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        Wave.SetActive(false);  
        Swirl.SetActive(false);
    }
    public void OnEnable()
    {
        isOnLiquidState = false;
        TransformAction.action.Enable();
        Hability.action.Enable();
    }
    private void OnDisable()
    {
        isOnLiquidState = false;
    }

    void Update()
    {
        if (TransformAction.action.triggered && canUltraJump) 
        {
            isOnUltraJumping = true;
            canUltraJump = false; 
            SwirlJump();
        }

        if (Hability.action.triggered && canTP && !isMovingTowardsObjective)
        {
            StartCoroutine(MoveTowardsObjective());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TeleportPoint"))
        {
            Debug.Log("Ha entrado para hacer TPPPPPPPP"); 
            canTP= true;
            Debug.Log("EL ESTADO DEL BOOLEANO PARA HACER TP ES:" + canTP);


        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TeleportPoint"))
        {
            canTP = false;
            Debug.Log("EL ESTADO DEL BOOLEANO PARA HACER TP ES:"+canTP);

        }

    }
    private void SwirlJump()
    {
        Debug.Log("Fiuuuuuuum super salto");
        if (isOnUltraJumping == true)
        {
            StartCoroutine(SwirlJumpCoroutine());
            isOnUltraJumping = false;
            Swirl.SetActive(true);
        }

    }
    IEnumerator SwirlJumpCoroutine()
    {
        animator.SetBool("TransformBool", true);
      
        yield return new WaitForSeconds(1f);
        controller.verticalVelocity = 20;
        yield return new WaitForSeconds(0.1f);


        while (!controller.characterController.isGrounded)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        controller.verticalVelocity = 0;
        animator.SetBool("TransformBool", false);
        Swirl.SetActive(false);
        isOnUltraJumping = false;
        canUltraJump = true;
    }

    //algo no funciona bieeen!! alv
    IEnumerator MoveTowardsObjective()
    {
        isMovingTowardsObjective = true;
        controller.characterController.enabled = false; 
        Wave.SetActive(true);

        GameObject[] objectives = GameObject.FindGameObjectsWithTag("PointToReach");
        Transform closestObjective = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        // garantiza que la primera distancia calculada se convierta automáticamente en la distancia más cercana hasta ahora ya que así no se va a las otras
        //con el Mathf-Infinity
        foreach (GameObject objective in objectives)
        {
            float distance = Vector3.Distance(currentPosition, objective.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObjective = objective.transform;
            }
        }

        if (closestObjective != null)
        {
            Vector3 objectivePosition = closestObjective.position;

            while (transform.position != objectivePosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, objectivePosition, Time.deltaTime * 5f);
                yield return null;
            }
        }
        Wave.SetActive(false);  
        controller.characterController.enabled = true; 
        isMovingTowardsObjective = false;
        canTP=false;

    }
}
