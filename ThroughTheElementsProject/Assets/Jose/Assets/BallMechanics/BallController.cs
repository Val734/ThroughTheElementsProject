using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del objeto
    private Rigidbody rb; // Referencia al componente Rigidbody del objeto

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtenemos el componente Rigidbody del objeto
    }

    void Update()
    {
        // Obtener el input de movimiento horizontal y vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Crear un vector de movimiento basado en el input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Aplicar la velocidad al vector de movimiento
        rb.velocity = movement * speed;
    }
}