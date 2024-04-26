using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReturnToSpawn : MonoBehaviour
{
    public BossBattleController battleController;
    public GameObject SpawnManager;
    public GameObject Player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("CannonBall"))
        {
            Debug.Log("Hola");
            if(other.CompareTag("CannonBall"))
            {
                GameObject Cannon = other.GetComponent<CannonBall>().Cannon;
                Cannon.GetComponent<CannonWorking>()._cam.gameObject.SetActive(false);
                Player.GetComponentInChildren<SolidStateBehaviour>().isOnBallTransformation = false;
                Destroy(other);
            }
            Player.SetActive(false);
            Player.transform.position=SpawnManager.GetComponent<SpawnManager>().currentSpawnPoint.transform.position;
            Player.SetActive(true);
            
            if(battleController)
            {
                Debug.Log("HA CAIDO EL TONTO DEL PLAYER");
                battleController.ChangeToStateWaiting();
            }
        }
    }

    //public void DiePlayer()
    //{
    //    Player.SetActive(false);
    //    Player.GetComponent<HealthBehaviour>().health = Player.GetComponent<HealthBehaviour>().maxHealth;
    //    Player.transform.position = SpawnManager.GetComponent<SpawnManager>().currentSpawnPoint.transform.position;
    //    Player.SetActive(true);
    //}
}
