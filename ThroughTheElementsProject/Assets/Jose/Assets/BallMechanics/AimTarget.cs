using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimingTarget : MonoBehaviour
{
    [SerializeField] LayerMask aimingLayerMask = Physics.DefaultRaycastLayers;
    public Camera mainCamera;
    public void Awake()
    {
    }
    void Update()
    {
        Vector2 screenPosition = Mouse.current.position.value;

        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,aimingLayerMask))
        {
            transform.position = hit.point;
        }
    }
}