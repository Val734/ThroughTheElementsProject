using System.Collections.Generic;
using UnityEngine;

public class KillEmAll : MonoBehaviour
{
    public List<GameObject> enemies;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    HealthBehaviour healthBehaviour = enemy.GetComponent<HealthBehaviour>();
                    if (healthBehaviour != null)
                    {
                        healthBehaviour.OnDie.Invoke();
                    }
                }
            }
        }
    }
}
