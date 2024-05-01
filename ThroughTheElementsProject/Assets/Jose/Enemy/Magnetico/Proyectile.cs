using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    protected Rigidbody rb;
    Transform player;
    float ForceSpeed = 320f;
    float aimbotForceSpeed = 15f;
    Transform targetPosition;

    void Awake()
    {
        Debug.Log("EL PROYECTIL HA HECHO EL AWAKE");
        if(player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            rb = GetComponent<Rigidbody>();

            targetPosition = player.transform;

        }
        

    }
    public void Throw(Vector3 horizontal, Vector3 vertical)
    {


        rb.AddForce(horizontal * ForceSpeed);

        Vector3 directionToPlayer = (targetPosition.position - transform.position).normalized;
        rb.AddForce(0f, 0f, directionToPlayer.x * aimbotForceSpeed);
    }
}
