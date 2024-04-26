using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OrbBehaviour : MonoBehaviour
{
    CharacterController _cController;

    Vector2 randomForce;
    Vector2 randomAscentForce;

    [SerializeField] float force;
    [SerializeField] float ascentForce = 10f;

    float lifeTime = 10f;

    [Header("Target Settings")]
    Vector3 orbTargetPosition;

    // LAUNCH SETTINGS
    bool isThrowed;
    float waitTime = 0.7f;

    private void Awake()
    {
        randomForce.x = 15f;
        randomForce.y = 10f;

        lifeTime = 10f;

        randomAscentForce.x = 10f;
        randomAscentForce.y = 8f; 

        _cController = GetComponent<CharacterController>();

        force = Random.Range(randomForce.x, randomForce.y);
    }

    public void ThrowOrb(Vector3 targetPosition)
    {
        StartCoroutine(Orblaunch());
        orbTargetPosition = targetPosition;
    }

    IEnumerator Orblaunch()
    {
        yield return new WaitForSeconds(waitTime);
        transform.parent = null;
        isThrowed = true;
    }

    private void Update()
    {
        if (isThrowed)
        {
            _cController.Move((orbTargetPosition - transform.position).normalized * (int)force * Time.deltaTime + Vector3.up * ascentForce * Time.deltaTime);
            
            lifeTime -= Time.deltaTime;
            if(lifeTime < 0) 
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Ha chocado y tendrá que hacer lo del Hurt Collider");

            Debug.Log("Ahora tiene que instanciar la Explosion de agua para que haga un efecto como que ha explotado el orbe");

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Ha chocado con algo que no es el player");
            Destroy (gameObject);
        }
    }
}

