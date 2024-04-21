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

    public void DiePlayer()
    {
        Player.SetActive(false);
        Player.GetComponent<HealthBehaviour>().health = Player.GetComponent<HealthBehaviour>().maxHealth;
        Player.transform.position = SpawnManager.GetComponent<SpawnManager>().currentSpawnPoint.transform.position;
        Player.SetActive(true);
    }
}
