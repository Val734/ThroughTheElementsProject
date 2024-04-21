using UnityEngine;
using Cinemachine;

public class CustomCameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public LayerMask layersToCull;
    public float cullDistance = 100f;

    void Start()
    {
        OverrideClipDistancesForFreeLookCamera();
    }

    void OverrideClipDistancesForFreeLookCamera()
    {
        if (freeLookCamera == null)
        {
            Debug.LogError("FreeLook camera not assigned!");
            return;
        }

        // Ajusta la distancia de culling para la cámara CinemachineFreeLook
        foreach (CinemachineVirtualCamera virtualCamera in freeLookCamera.GetComponentsInChildren<CinemachineVirtualCamera>())
        {
            // Verifica si la capa de la cámara coincide con las capas especificadas para culling
            if (((10 << virtualCamera.gameObject.layer) & layersToCull) != 0)
            {
                // Ajusta las propiedades de culling de la cámara virtual
                virtualCamera.m_Lens.NearClipPlane = 0.01f; // Establece la distancia cercana
                virtualCamera.m_Lens.FarClipPlane = cullDistance; // Establece la distancia lejana
            }
        }
    }
}
