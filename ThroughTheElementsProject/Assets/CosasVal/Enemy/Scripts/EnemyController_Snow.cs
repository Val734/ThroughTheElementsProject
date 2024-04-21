using System.Collections;
using UnityEngine;

public class EnemyController_Snow : EnemyController
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] GameObject hitCollider;

    bool activated;
    float localSpeed = 0f;
    Vector3 initialPosition;

    protected override void ChildAwake()
    {
        activated = false;
        initialPosition = transform.position; // Guarda la posición inicial
    }

    protected override void ChildUpdate()
    {
        // Aquí puedes agregar lógica adicional para el comportamiento de patrulla
    }

    protected override float ReturnSpeed()
    {
        return localSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            rb.AddForce(Vector3.up * 350f);
            StartCoroutine(ChangeColliders());
        }
        else if (other.CompareTag("Player"))
        {
            StartCoroutine(waitToAttack());
        }
    }

    IEnumerator ChangeColliders()
    {
        yield return new WaitForSeconds(1);
        capsuleCollider.enabled = false;
        sphereCollider.enabled = true;
        characterController.enabled = true;
        rb.isKinematic = true;
        activated = true;

        // Espera un tiempo y luego regresa a la posición inicial
        yield return new WaitForSeconds(5); // Cambia esto según cuánto tiempo quieres esperar antes de volver
        ReturnToInitialPosition();
    }

    IEnumerator waitToAttack()
    {
        localSpeed = 0;
        hitCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        hitCollider.gameObject.SetActive(false);
        localSpeed = 0;
    }

    void ReturnToInitialPosition()
    {
        // Vuelve al punto inicial
        //transform.position = initialPosition;
        activated = false;
        // Reactiva la cápsula y desactiva la esfera para volver al comportamiento inicial
        capsuleCollider.enabled = true;
        sphereCollider.enabled = false;
        // Aquí puedes agregar lógica adicional, como reiniciar la velocidad de patrulla
    }
}
