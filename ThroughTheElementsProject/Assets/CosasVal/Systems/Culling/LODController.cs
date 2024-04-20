using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODController : MonoBehaviour
{
    public GameObject CheckPointToActivate;
    public Transform player;
    public float distanceThreshold = 30f;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < distanceThreshold)
        {
            CheckPointToActivate.SetActive(true);
        }
        else
        {
            CheckPointToActivate.SetActive(false);
        }
    }
}
