using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashBehaviour : MonoBehaviour
{
    CharacterController cController;
    [SerializeField] float speed = 4f;
    Vector3 targetPosition;

    HitCollider hitCollider;

    [SerializeField] float spawnTime = 2f;

    public GameObject onDestroyExplosion;

    private void Awake()
    {
        hitCollider = GetComponent<HitCollider>();
        cController = GetComponent<CharacterController>();

        // PODRIA AÑADIR UN ADDLISTENER PARA ASI DESTRUIR EL SLASH SI TOCA AL PLAYER

        Destroy(this, 1f);
    }

    private void OnDestroy()
    {
        GameObject explosion = Instantiate(onDestroyExplosion, transform.position, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * 20;
        Destroy(explosion, 1f);
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
}
