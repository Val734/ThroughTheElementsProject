using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurbineBehaviour : MonoBehaviour
{
    [SerializeField] List<string> hittableTags = new List<string> { "Player", "PunchingBag" };
    [SerializeField] public UnityEvent<Vector3,float> onStart;
    [SerializeField] public UnityEvent onStop;
    [SerializeField] float trubineSpeed = 20f;
    private void OnTriggerEnter(Collider other)
    {
        StartTurbine(other);
    }
    private void OnTriggerExit(Collider other)
    {

        StopTurbine();
    }
    private void StartTurbine(Collider other)
    {
        if (hittableTags.Contains(other.tag))
        {
            onStart.Invoke(transform.forward, trubineSpeed);
        }
    }
    private void StopTurbine()
    {
        onStop.Invoke();
    }
}
