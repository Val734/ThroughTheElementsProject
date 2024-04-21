using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Turbine_DetectCollider : MonoBehaviour
{
    [SerializeField] EnemyController_Turbine Enemy;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("El Player ha sido detectado");
            Enemy.GetComponent<EnemyController_Turbine>().CollisionDetected();
        }
    }
}
