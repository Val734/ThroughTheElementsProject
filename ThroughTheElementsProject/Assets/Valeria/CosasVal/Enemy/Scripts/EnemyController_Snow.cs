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
        initialPosition = transform.position; // Guarda la posici�n inicial
    }

    protected override void ChildUpdate()
    {
        // Aqu� puedes agregar l�gica adicional para el comportamiento de patrulla
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

        // Espera un tiempo y luego regresa a la posici�n inicial
        yield return new WaitForSeconds(5); // Cambia esto seg�n cu�nto tiempo quieres esperar antes de volver
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
        // Reactiva la c�psula y desactiva la esfera para volver al comportamiento inicial
        capsuleCollider.enabled = true;
        sphereCollider.enabled = false;
        // Aqu� puedes agregar l�gica adicional, como reiniciar la velocidad de patrulla
    }
}
