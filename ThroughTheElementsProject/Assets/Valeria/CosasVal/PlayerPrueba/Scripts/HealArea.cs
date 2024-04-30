using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class HealArea : MonoBehaviour
{
    [SerializeField] UnityEvent onHeal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("aaaaaaaa");
            onHeal.Invoke();
        }
    }
}
