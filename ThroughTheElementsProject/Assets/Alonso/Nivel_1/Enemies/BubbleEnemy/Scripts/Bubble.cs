using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Proyectile
{
    float lifeTime = 10f;

    bool playerHitted = false;
    public float goingUpTime = 4f;

    public float verticalForce = 1f;
    float intervalAscentTime = 0.1f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHitted = true;
            collision.GetComponent<CharacterController>().enabled = false;
            collision.GetComponent<BubbleDragBehaviour>().ActivateBubbleDragged(transform, goingUpTime);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (playerHitted)
        {
            goingUpTime -= Time.deltaTime;
            if (goingUpTime < 0)
            {
                Destroy(gameObject);
            }

            rb.useGravity = true;
            rb.isKinematic = false;

            rb.velocity = Vector3.up * verticalForce;
        }

        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy (gameObject);
        }    
    }
}
