using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Proyectile
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
