using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : Proyectile
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Ground"))
        {
                Destroy(gameObject);
        }
    }
}
