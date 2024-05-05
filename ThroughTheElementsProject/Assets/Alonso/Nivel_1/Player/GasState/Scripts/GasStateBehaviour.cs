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


    [Header("Sound Settings")]
    [SerializeField] GameObject soundManager;
    private AudioSource throwGas;
    private AudioSource geiser;


    public void Awake()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        geiser = soundManager.transform.Find("GeiserFlying").GetComponent<AudioSource>();
        throwGas = soundManager.transform.Find("GasEffect").GetComponent<AudioSource>();

    }

    public void OnEnable()
    {
        isOnGasState = false;
        TransformAction.action.Enable();
        Hability.action.Enable();
        GasTrowed.gameObject.SetActive(true);
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
            geiser.Stop(); 
            animator.SetBool("TransformBool", false);

        }


    }

    private void ThrowGas()
    {
        Vector3 spawnPosition = transform.position + transform.rotation * Vector3.forward * 2.0f; // Ajusta el valor para la distancia deseada
        // Instancia el gas en la nueva posición calculada
        Instantiate(GasTrowed, spawnPosition, transform.rotation);
        animator.SetTrigger("ThrowGas");
        throwGas.Play(); 
    }

    IEnumerator Gas()
    {
        yield return new WaitForSeconds(0.8f);
        controller.gravity = -1;
        controller.canJump = false;

        isOnGasState = true;
        Fog.SetActive(true);
        geiser.Play(); 
        Debug.Log("ACTIVADO EL FOG");
    }

}
