using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Move : MonoBehaviour
{
    public float moveSpeed = 5f;         // Velocidad de movimiento lateral
    public float rotationSpeed = 100f;   // Velocidad de rotaci�n del bal�n
    public float minMoveThreshold = 0.1f; // Velocidad m�nima para que se considere en movimiento
    public Transform cameraTransform;    // Referencia a la c�mara

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody del bal�n
    }

    void Update()
    {
        // Verificar si el bal�n est� en movimiento (hacia adelante)
        if (rb.velocity.magnitude > minMoveThreshold)
        {
            // Obt�n la entrada horizontal (A/D o Flechas izquierda/derecha)
            float horizontalInput = Input.GetAxis("Horizontal");

            if (horizontalInput != 0)
            {
                // Calcula la direcci�n lateral en base a la c�mara
                Vector3 rightDirection = cameraTransform.right;
                rightDirection.y = 0; // Limitar el movimiento al plano horizontal
                rightDirection.Normalize();

                // A�adir torque para girar el bal�n sin frenar su avance
                Vector3 torqueDirection = cameraTransform.forward * horizontalInput * -rotationSpeed;
                rb.AddTorque(torqueDirection, ForceMode.Force); // Usa "Force" para suavidad

                // A�adir un peque�o ajuste lateral para que se desplace suavemente a la izquierda o derecha sin frenar
                Vector3 moveDirection = rightDirection * horizontalInput * moveSpeed * 0.1f;
                rb.AddForce(moveDirection, ForceMode.VelocityChange); // A�adir ajuste suave sin interferir con la velocidad
            }
        }
    }
}