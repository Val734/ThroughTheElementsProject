using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBehaviour : MonoBehaviour
{
    private PlayerController controller;
    private float originalGravity;

    public float geiserForce;

    public void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<GasStateBehaviour>().enabled == true)
        {
            originalGravity = other.GetComponent<PlayerController>().gravity;
            other.GetComponent<PlayerController>().gravity = geiserForce;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().gravity = originalGravity;
        }
    }
}
