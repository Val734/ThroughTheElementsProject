using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Turbine : EnemyController
{
    [Header("Atacking Settings")]
    [SerializeField] BoxCollider attackbCollider;
    [SerializeField] float localSpeed;

    protected override void ChildAwake()
    {
        localSpeed = 0;
    }

    protected override void ChildUpdate()
    {
    }

    protected override float ReturnSpeed()
    {
        return localSpeed;
    }

    public void CollisionDetected()
    {
        StartCoroutine(waitToAttack());
    }

    IEnumerator waitToAttack()
    {
        yield return new WaitForSeconds(2);
        attackbCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        attackbCollider.gameObject.SetActive(false);
    }
}
