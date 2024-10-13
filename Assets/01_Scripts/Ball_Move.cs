using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Move : MonoBehaviour
{
    public float moveSpeed = 5f;         // Velocidad de movimiento lateral
    public float rotationSpeed = 100f;   // Velocidad de rotación del balón
    public float minMoveThreshold = 0.1f; // Velocidad mínima para que se considere en movimiento
    public Transform cameraTransform;    // Referencia a la cámara

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody del balón
    }

    void Update()
    {
        // Verificar si el balón está en movimiento (hacia adelante)
        if (rb.velocity.magnitude > minMoveThreshold)
        {
            // Obtén la entrada horizontal (A/D o Flechas izquierda/derecha)
            float horizontalInput = Input.GetAxis("Horizontal");

            if (horizontalInput != 0)
            {
                // Calcula la dirección lateral en base a la cámara
                Vector3 rightDirection = cameraTransform.right;
                rightDirection.y = 0; // Limitar el movimiento al plano horizontal
                rightDirection.Normalize();

                // Añadir torque para girar el balón sin frenar su avance
                Vector3 torqueDirection = cameraTransform.forward * horizontalInput * -rotationSpeed;
                rb.AddTorque(torqueDirection, ForceMode.Force); // Usa "Force" para suavidad

                // Añadir un pequeño ajuste lateral para que se desplace suavemente a la izquierda o derecha sin frenar
                Vector3 moveDirection = rightDirection * horizontalInput * moveSpeed * 0.1f;
                rb.AddForce(moveDirection, ForceMode.VelocityChange); // Añadir ajuste suave sin interferir con la velocidad
            }
        }
    }
}