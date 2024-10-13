using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float zoomSpeed = 10f;           // Velocidad del zoom
    public float minZoomDistance = 5f;      // Distancia mínima de la cámara
    public float maxZoomDistance = 50f;     // Distancia máxima de la cámara
    public float moveSpeed = 0.5f;          // Velocidad de movimiento de la cámara
    public float rotationSpeed = 100f;      // Velocidad de rotación de la cámara
    public float followSpeed = 5f;          // Velocidad a la que la cámara sigue al objeto
    public float initialDistanceFromTarget = 20f; // Distancia inicial de la cámara desde el objeto

    private Vector3 dragOrigin;
    private bool isManualControl = false;   // Variable para determinar si la cámara está siendo controlada manualmente
    public float manualControlDuration = 1.5f; // Tiempo que la cámara deja de seguir tras moverse manualmente
    private float manualControlTimer = 0f;

    public Transform target;  // Objeto a seguir

    void Start()
    {
        if (target != null)
        {
            // Establece la posición inicial de la cámara a una distancia inicial desde el objeto
            transform.position = target.position - transform.forward * initialDistanceFromTarget;
        }
    }

    void Update()
    {
        // Control de zoom con la rueda del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            isManualControl = true;
            manualControlTimer = manualControlDuration;

            // Calcula la nueva posición de la cámara
            Vector3 direction = transform.forward * scroll * zoomSpeed;
            Vector3 newPosition = transform.position + direction;

            // Calcula la distancia entre la nueva posición y el objeto que estás viendo
            float distanceToTarget = Vector3.Distance(newPosition, target.position);

            // Limitar el zoom basado en la distancia mínima y máxima
            if (distanceToTarget > minZoomDistance && distanceToTarget < maxZoomDistance)
            {
                transform.position = newPosition;
            }
        }

        // Movimiento de la cámara con el click derecho basado en la dirección de la cámara
        if (Input.GetMouseButtonDown(1))
        {
            isManualControl = true;
            manualControlTimer = manualControlDuration;
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);

            // Mover la cámara según la dirección a la que mira
            Vector3 move = transform.right * difference.x * moveSpeed + transform.up * difference.y * moveSpeed;
            transform.Translate(move, Space.World);

            dragOrigin = Input.mousePosition;
        }

        // Rotación de la cámara con el click central
        if (Input.GetMouseButton(2))
        {
            isManualControl = true;
            manualControlTimer = manualControlDuration;

            float rotateHorizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotateVertical = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotateHorizontal, Space.World);
            transform.Rotate(Vector3.right, rotateVertical, Space.Self);
        }

        // Actualizar el temporizador de control manual
        if (manualControlTimer > 0)
        {
            manualControlTimer -= Time.deltaTime;
        }
        else
        {
            isManualControl = false;
        }
    }

    void LateUpdate()
    {
        // Solo sigue al objeto si no hay control manual
        if (target != null && !isManualControl)
        {
            // Solo actualiza la posición de la cámara para seguir al objeto suavemente
            Vector3 targetPosition = target.position - transform.forward * initialDistanceFromTarget;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
