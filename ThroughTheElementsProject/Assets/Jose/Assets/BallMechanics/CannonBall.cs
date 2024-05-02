using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonBall : MonoBehaviour
{
    public GameObject Player;
    public GameObject Cannon;
    private GameObject NextCannon;


    public void Start()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(Coll());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            Player.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3.5f, gameObject.transform.position.z);
            Player.SetActive(true);
            Cannon.GetComponent<CannonWorking>()._cam.gameObject.SetActive(false);
            Player.GetComponentInChildren<SolidStateBehaviour>().isOnBallTransformation = false;
            NextCannon = null;
            Destroy(gameObject);

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cannon"))
        {

            //Player.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2f, gameObject.transform.position.z);
            //Player.SetActive(true);
            Player.GetComponentInChildren<SolidStateBehaviour>().isOnBallTransformation = true;
            other.gameObject.GetComponentInChildren<CannonWorking>().CannonPlayer(Player);
            NextCannon = other.gameObject;
            StartCoroutine(CameraCannon()); 
        }
    }
    IEnumerator Coll()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Collider>().enabled = true;

    }
    IEnumerator CameraCannon()
    {
        yield return new WaitForSeconds(0.1f);
        if (Cannon.gameObject != NextCannon.gameObject)
        {

            Cannon.GetComponent<CannonWorking>()._cam.gameObject.SetActive(false);

        }
        Destroy(gameObject);


    }
}
