using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaBehaviour : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad de rotaci�n

    void Update()
    {
        // Rotar el objeto alrededor del eje Y (vertical) a una velocidad constante
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
