using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject Boss;
    public GameObject fractured;

    public void FractureObject()
    {
        Debug.Log("hOLA");
        Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
        Destroy(gameObject); //Destroy the object to stop it getting in the way
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CannonBall"))
        {

            FractureObject();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            if (Boss != null)
            {
                Boss.GetComponent<BossBehaviour>().StopSpecialAttack();
                Boss.GetComponent<HealthBehaviour>().Damage(5);
            }
            FractureObject();

        }
    }

}
