using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eNDcREDITS : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(EndCredits());
    }
    IEnumerator EndCredits()
    {
        yield return new WaitForSeconds(60);

        GoTo.GoToGameplay();
    }
}
