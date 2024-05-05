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

    Collider Player;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision;
            playerHitted = true;
            collision.GetComponent<PlayerController>().enabled = false;
            collision.GetComponent<CharacterController>().enabled = false;
            collision.GetComponent<BubbleDragBehaviour>().ActivateBubbleDragged(transform, goingUpTime);
            collision.GetComponentInChildren<Animator>().SetBool("BubbleAttack", true);
            transform.localScale = Vector3.one * 2.5f;
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
                Player.GetComponent<PlayerController>().enabled = true;
                Player.GetComponentInChildren<Animator>().SetBool("BubbleAttack", false);
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
