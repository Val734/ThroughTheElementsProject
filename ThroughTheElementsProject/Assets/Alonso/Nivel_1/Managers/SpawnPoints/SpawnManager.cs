using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] Transform initialSpawnPoint;
    [SerializeField] public Transform currentSpawnPoint;
    [SerializeField] List<Transform> SpawnPoints;

    [Header("Player")]
    [SerializeField] Transform Player; //AQU� HABR� QUE ARRASTRAR AL PLAYER PARA QUE PODAMOS MOVERLO CADA VEZ


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
        Player.position = currentSpawnPoint.position;
        PRUEBAS = false;
    }
}
