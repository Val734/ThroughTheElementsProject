using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBehaviour : MonoBehaviour
{
    [SerializeField] float gasSpeed = 5000f;
    [SerializeField] float gasLifeTime = 4f;

    [Header("Attack Settings")]
    [SerializeField] float intervalTime = 1f;
    [SerializeField] float colliderEnabledTime;
    [SerializeField] float colliderDisabledTime;


    Rigidbody rb;
    SphereCollider sphereCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = rb.GetComponent<SphereCollider>();
        sphereCollider.enabled = true;
    }


    private void Update()
    {
        rb.MovePosition(transform.position + transform.forward * gasSpeed * Time.deltaTime);

        colliderEnabledTime -= Time.deltaTime;
        if(colliderEnabledTime < 0)
        {
            sphereCollider.enabled = false;

            colliderDisabledTime -= Time.deltaTime;
            if(colliderDisabledTime < 0)
            {
                sphereCollider.enabled = true;
                colliderEnabledTime = intervalTime;
                colliderDisabledTime = intervalTime;
            }
        }


        gasLifeTime -= Time.deltaTime;
        if(gasLifeTime < 0) 
        {
            Destroy(gameObject);
        }
    }

}
