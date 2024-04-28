using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashBehaviour : MonoBehaviour
{
    CharacterController cController;
    [SerializeField] float speed = 4f;
    Vector3 targetPosition;

    [SerializeField] float spawnTime = 2f;

    private void Awake()
    {
        cController = GetComponent<CharacterController>();
        Destroy(this, 2f);
    }

    public void ThrowSlash(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    { 
        if (targetPosition != null)
        {
            cController.Move((targetPosition - transform.position) * speed * Time.deltaTime);
        }       
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if(hit.gameObject.CompareTag("Player")) 
    //    {
    //        int damage = this.GetComponent<HitCollider>().damage;

    //        hit.gameObject.GetComponent<HurtCollider>().NotifyHit(this., damage);

    //        Debug.Log("HE TOCADO AL PLAYER");
    //    }
    //}
}
