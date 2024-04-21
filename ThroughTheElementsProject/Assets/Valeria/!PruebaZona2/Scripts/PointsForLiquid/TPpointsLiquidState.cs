using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TPpointsLiquidState : MonoBehaviour
{
    [SerializeField] UnityEvent EnablePoint;
    [SerializeField] UnityEvent DisablePoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnablePoint.Invoke(); 
        }
    }
}
