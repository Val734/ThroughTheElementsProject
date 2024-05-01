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

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
        CheckHit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        CheckHit(collision.collider);
    }

    private void CheckHit(Collider other)
    {
        if (hittableTags.Contains(other.tag))
        {
            other.GetComponentInChildren<HurtCollider>()?.NotifyHit(this,damage);
            OnhittableObject.Invoke();
            Debug.Log($"CheckHit {other.gameObject}");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}