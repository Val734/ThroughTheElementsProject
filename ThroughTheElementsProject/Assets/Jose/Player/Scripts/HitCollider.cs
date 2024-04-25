using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HitCollider : MonoBehaviour
{
    [SerializeField]
    List<string> hittableTags = new List<string>{ "PunchingBag","Enemy","Player"};
    public UnityEvent OnhittableObject;
    public int damage;
    private bool canHit;

    void OnEnable()
    {
        canHit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckHit(other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        CheckHit(collision.collider);
    }

    private void CheckHit(Collider other)
    {
        if (hittableTags.Contains(other.tag) && canHit)
        {
            other.GetComponentInChildren<HurtCollider>()?.NotifyHit(this,damage);
            OnhittableObject.Invoke();
            canHit = false;

        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}