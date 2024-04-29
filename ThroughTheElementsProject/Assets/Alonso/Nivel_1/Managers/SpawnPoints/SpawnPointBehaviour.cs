using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPointBehaviour : MonoBehaviour
{
    [SerializeField] SpawnManager _spawnManager;
    public UnityEvent OnSpawn;


    private void Awake()
    {
        _spawnManager = GetComponentInParent<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _spawnManager.ChangeSpawnPoint(transform);
            gameObject.SetActive(false);
            OnSpawn.Invoke();
        }
    }
}
