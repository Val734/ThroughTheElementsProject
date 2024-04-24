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
    HitCollider hitCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = rb.GetComponent<SphereCollider>();
        hitCollider = rb.GetComponent<HitCollider>();

        hitCollider.OnhittableObject.AddListener(PruebaHitCollider);

        sphereCollider.enabled = true;
        hitCollider.enabled = true;
    }


    private void Update()
    {
        rb.MovePosition(transform.position + transform.forward * gasSpeed * Time.deltaTime);

        colliderEnabledTime -= Time.deltaTime;
        if(colliderEnabledTime < 0)
        {
            sphereCollider.enabled = false;
            hitCollider.enabled= false;

            colliderDisabledTime -= Time.deltaTime;
            if(colliderDisabledTime < 0)
            {
                sphereCollider.enabled = true;
                hitCollider.enabled = true;
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

    public void PruebaHitCollider()
    {
        Debug.Log("HA HECHO UN HIT COLLIDER");
    }

}
