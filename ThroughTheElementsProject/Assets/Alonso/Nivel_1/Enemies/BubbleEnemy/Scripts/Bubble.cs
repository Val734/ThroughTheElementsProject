using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Proyectile
{
    bool playerHitted = false;
    public float goingUpTime = 4f;

    public float verticalForce = 1f;
    float intervalAscentTime = 0.1f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHitted = true;
            collision.GetComponent<BubbleDragBehaviour>().ActivateBubbleDragged(transform, goingUpTime);
        }
        else
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
    }
}
