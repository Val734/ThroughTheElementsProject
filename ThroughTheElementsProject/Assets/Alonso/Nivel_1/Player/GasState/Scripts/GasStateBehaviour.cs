using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GasStateBehaviour : MonoBehaviour
{
    private PlayerController controller;
    private bool isOnGasState;
    [SerializeField] InputActionReference TransformAction;
    [SerializeField] InputActionReference Hability;

    public GameObject GasTrowed;
    public GameObject Fog;

    private Animator animator;

    public void Awake()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
    }

    public void OnEnable()
    {
        isOnGasState = false;
        TransformAction.action.Enable();
        Hability.action.Enable();
    }

    private void OnDisable()
    {
        controller.gravity = -9.8f;
        controller.canJump = true;
        isOnGasState = false;
        GasTrowed.gameObject.SetActive(false);
        Fog.gameObject.SetActive(false);
    }

    void Update()
    {
        if (TransformAction.action.triggered)
        {
            ChangeGravity();

        }
        else if (!isOnGasState && Hability.action.triggered)
        {
            ThrowGas();

        }
    }
    private void ChangeGravity()
    {
        if (!isOnGasState)
        {
            StartCoroutine(Gas());

            animator.SetBool("TransformBool", true);
        }
        else if (isOnGasState)
        {
            controller.gravity = -9.81f;
            controller.canJump = true;
            isOnGasState = false;
            Fog.SetActive(false);
            animator.SetBool("TransformBool", false);

        }


    }

    private void ThrowGas()
    {
        Vector3 spawnPosition = transform.position + transform.rotation * Vector3.forward * 2.0f; // Ajusta el valor para la distancia deseada
        // Instancia el gas en la nueva posición calculada
        Instantiate(GasTrowed, spawnPosition, transform.rotation);
    }

    IEnumerator Gas()
    {
        yield return new WaitForSeconds(0.8f);
        controller.gravity = -1;
        controller.canJump = false;

        isOnGasState = true;
        Fog.SetActive(true);
        Debug.Log("ACTIVADO EL FOG");
    }

}
