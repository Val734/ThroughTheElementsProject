using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReturnToSpawn : MonoBehaviour
{
    public GameObject SpawnManager;
    public GameObject Player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("CannonBall"))
        {
            Debug.Log("Hola");
            Player.SetActive(false);
            Player.transform.position=SpawnManager.GetComponent<SpawnManager>().currentSpawnPoint.transform.position;
            Player.SetActive(true);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hola");

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("CannonBall"))
        {
            Player.SetActive(true);
            Player.transform.position = SpawnManager.GetComponent<SpawnManager>().currentSpawnPoint.transform.position;
        }
    }
}
