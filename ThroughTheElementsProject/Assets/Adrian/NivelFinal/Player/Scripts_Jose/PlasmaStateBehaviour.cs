using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlasmaStateBehaviour : MonoBehaviour
{
    [SerializeField] InputActionReference TransformAction;
    [SerializeField] InputActionReference Hability;

    [SerializeField] float detectionRadiusPlasmaOrb = 20f;
    Collider[] objetosCercanos;
    Transform closestPlasmaOrb;
    float closestPlasmaOrbDistance;

    PlayerController controller;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
    public void OnEnable()
    {
        TransformAction.action.Enable();
        Hability.action.Enable();
    }
    private void OnDisable()
    {
        TransformAction.action.Disable();
        Hability.action.Disable();
    }
    void Update()
    {
        if(Hability.action.IsPressed())
        {
            objetosCercanos = Physics.OverlapSphere(transform.position, detectionRadiusPlasmaOrb);
            Debug.Log(objetosCercanos.Length);
            if(objetosCercanos != null)
            {
                closestPlasmaOrbDistance = Vector3.Distance(transform.position, objetosCercanos[0].transform.position);

                foreach (Collider obj in objetosCercanos)
                {
                    if (obj.CompareTag("PlasmaOrb"))
                    {
                        if(Vector3.Distance(transform.position, obj.transform.position) < closestPlasmaOrbDistance)
                        {
                            closestPlasmaOrb = obj.transform;
                            closestPlasmaOrbDistance = Vector3.Distance(transform.position, obj.transform.position);
                        }
                    }
                }
                Debug.Log(closestPlasmaOrb);
                if (closestPlasmaOrb != null)
                {
                    while (Vector3.Distance(transform.position, closestPlasmaOrb.transform.position)>=2)
                    {
                        controller.characterController.Move((closestPlasmaOrb.position-transform.position).normalized);
                    }
                }
            }
        }
    }
}
