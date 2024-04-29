using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatform : MonoBehaviour
{
    public bool playerIsInPlatform;
    public GameObject healthbar;
    public GameObject Escenary;
    public GameObject timeLine;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsInPlatform = true;
            healthbar.SetActive(true);
            timeLine.SetActive(true);  
            Escenary.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInPlatform = false;

        }
    }
}
