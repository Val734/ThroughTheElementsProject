using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VacummBehaviour : MonoBehaviour
{
    [SerializeField] List<string> hittableTags = new List<string> { "Player", "PunchingBag" };
    [SerializeField] public UnityEvent<Vector3, float> onStart;
    [SerializeField] public UnityEvent onStop;
    [SerializeField] float vacummSpeed = 10f;
    private void OnTriggerStay(Collider other)
    {
        StartVacumm(other);
    }
    private void OnTriggerExit(Collider other)
    {
        StopVacumm();
    }
    private void StartVacumm(Collider other)
    {
        if (hittableTags.Contains(other.tag))
        {
            onStart.Invoke(transform.forward, vacummSpeed);
        }
    }
    private void StopVacumm()
    {
        onStop.Invoke();
    }
}
