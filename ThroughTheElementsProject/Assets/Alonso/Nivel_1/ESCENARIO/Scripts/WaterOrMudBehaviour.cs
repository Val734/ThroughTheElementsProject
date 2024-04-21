using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOrMudBehaviour : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("HA CHOCADO EL "+ other.gameObject.name);
    }
}
