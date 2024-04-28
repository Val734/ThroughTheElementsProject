using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    public GameObject onDestroyExplosion;
    float colliderActivatedTime = 0.5f;
    BoxCollider collider;

    private void Awake()
    {
        Destroy(gameObject, 2f);
        collider = GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        colliderActivatedTime -= Time.deltaTime;
        if (colliderActivatedTime < 0) 
        {
            if (collider != null) 
            {
                collider.isTrigger = false;
            }

        }
    }
}
