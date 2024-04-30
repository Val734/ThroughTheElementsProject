using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events; 

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] Transform initialSpawnPoint;
    [SerializeField] public Transform currentSpawnPoint;
    [SerializeField] List<Transform> SpawnPoints;

    [Header("Player")]
    [SerializeField] Transform Player; //AQUÍ HABRÁ QUE ARRASTRAR AL PLAYER PARA QUE PODAMOS MOVERLO CADA VEZ


    [Header("PRUEBA")]
    public bool PRUEBAS;


    private void OnValidate()
    {
        if(PRUEBAS) 
        {
            RespawnPlayer();
        }
    }


    private void Awake()
    {
        for(int i = 0;i < transform.childCount;i++) 
        {
            SpawnPoints.Add(transform.GetChild(i));
        }

        initialSpawnPoint = transform.GetChild(0);
        currentSpawnPoint = transform.GetChild(0);

    }

     
    public void ChangeSpawnPoint(Transform newSpawnPoint)
    {
        currentSpawnPoint = newSpawnPoint;

    }


    public void RespawnPlayer()
    {
        Player.gameObject.SetActive(false);
        Player.gameObject.transform.position = currentSpawnPoint.transform.position;
        Player.gameObject.GetComponent<PlayerController>().RestorePlayer();
        Player.gameObject.SetActive(true);
    }

    public void DiePlayer()
    {
        Debug.Log("Hola");
        Player.gameObject.SetActive(false);
        Player.gameObject.GetComponent<HealthBehaviour>().RestoreHealth();
        Player.gameObject.transform.position = currentSpawnPoint.transform.position;
        Player.gameObject.GetComponent<PlayerController>().RestorePlayer();
        Player.gameObject.SetActive(true);
    }
    
        
        
        
}
