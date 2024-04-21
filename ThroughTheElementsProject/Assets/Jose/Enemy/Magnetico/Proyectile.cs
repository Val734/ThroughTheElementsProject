using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    protected Rigidbody rb;
    Transform player;
    float ForceSpeed = 320f;
    float aimbotForceSpeed = 15f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }
    public void Throw(Vector3 horizontal, Vector3 vertical)
    {
        rb.AddForce(Vector3.forward * ForceSpeed);

    }
    private void Update()
    {
        Vector3 directionToPlayer=(player.position - transform.position).normalized;
        rb.AddForce(directionToPlayer.x*aimbotForceSpeed,0f,0f);
    }
}
