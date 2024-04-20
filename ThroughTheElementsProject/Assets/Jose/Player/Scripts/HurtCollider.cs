using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    [SerializeField] public UnityEvent<int> onHurt;
    internal void NotifyHit(HitCollider hitCollider, int damage)
    {
        Debug.Log("Hola");
        onHurt.Invoke(damage);

    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
