using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    public GameObject onDestroyExplosion;
    float colliderActivatedTime = 0.5f;
    BoxCollider collider;

    float lifeTime = 2f;

    private void Awake()
    {
        collider = GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        colliderActivatedTime -= Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (colliderActivatedTime < 0) 
        {
            if (collider != null) 
            {
                collider.isTrigger = false;
            }
        }

        if(lifeTime < 0) 
        {
            GameObject explosion = Instantiate(onDestroyExplosion, transform.position,Quaternion.identity);
            Destroy(explosion, 1f);

            Destroy(gameObject);
        }
    }
}
