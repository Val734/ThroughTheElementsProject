using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravitySphere : MonoBehaviour
{
    public bool BigGravitySphere;
    public bool canDestroy;
    private Rigidbody rb;
    private GameObject Player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        if(!BigGravitySphere)
        {
            Destroy(gameObject,4);
        }
        else if (BigGravitySphere && canDestroy)
        {
            StartCoroutine(DestroyBigBall());

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ThrowBall(Vector3 direction)
    {
        rb.AddForce(direction * 3, ForceMode.VelocityChange);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (BigGravitySphere)
        {
            if (other.CompareTag("Player"))
            {
                Player=other.gameObject;
                other.GetComponent<PlayerController>().gravity = -20;
                other.GetComponent<PlayerController>().movementSpeed = 1f;
                other.GetComponent<PlayerController>().runMovementSpeed = 3f;
                other.GetComponent<PlayerController>().dashingSpeed = 8;
                Debug.Log("ENtra");
            }
            
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().gravity = -9.81f;
            other.GetComponent<PlayerController>().movementSpeed = 4f;
            other.GetComponent<PlayerController>().runMovementSpeed = 10f;
            other.GetComponent<PlayerController>().dashingSpeed = 16;
        }
    }
   
    private void Restore()
    {
        Player.GetComponent<PlayerController>().gravity = -9.81f;
        Player.GetComponent<PlayerController>().movementSpeed = 4f;
        Player.GetComponent<PlayerController>().runMovementSpeed = 10f;
        Player.GetComponent<PlayerController>().dashingSpeed = 16;
    }
    IEnumerator DestroyBigBall()
    {
        yield return new WaitForSeconds(7);
        if(Player!=null)
        {
            Player.GetComponent<PlayerController>().gravity = -9.81f;
            Player.GetComponent<PlayerController>().movementSpeed = 4f;
            Player.GetComponent<PlayerController>().runMovementSpeed = 10f;
            Player.GetComponent<PlayerController>().dashingSpeed = 16;
        }
        
        Destroy(gameObject);
    }
  
}
