using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class eNDcREDITS : MonoBehaviour
{
    [SerializeField] UnityEvent onEndCredits;
    private void Start()
    {
        StartCoroutine(EndCredits());
    }
    IEnumerator EndCredits()
    {
        yield return new WaitForSeconds(60);
        onEndCredits.Invoke();
    }
}
