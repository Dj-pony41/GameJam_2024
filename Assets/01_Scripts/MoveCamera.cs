using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float zoomSpeed = 10f;           // Velocidad del zoom
    public float minZoomDistance = 5f;      // Distancia m�nima de la c�mara
    public float maxZoomDistance = 50f;     // Distancia m�xima de la c�mara
    public float moveSpeed = 0.5f;          // Velocidad de movimiento de la c�mara
    public float rotationSpeed = 100f;      // Velocidad de rotaci�n de la c�mara
    public float followSpeed = 5f;          // Velocidad a la que la c�mara sigue al objeto
    public float initialDistanceFromTarget = 20f; // Distancia inicial de la c�mara desde el objeto

    private Vector3 dragOrigin;
    private bool isManualControl = false;   // Variable para determinar si la c�mara est� siendo controlada manualmente
    public float manualControlDuration = 1.5f; // Tiempo que la c�mara deja de seguir tras moverse manualmente
    private float manualControlTimer = 0f;

    public Transform target;  // Objeto a seguir

    void Start()
    {
        if (target != null)
        {
            // Establece la posici�n inicial de la c�mara a una distancia inicial desde el objeto
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

            // Calcula la nueva posici�n de la c�mara
            Vector3 direction = transform.forward * scroll * zoomSpeed;
            Vector3 newPosition = transform.position + direction;

            // Calcula la distancia entre la nueva posici�n y el objeto que est�s viendo
            float distanceToTarget = Vector3.Distance(newPosition, target.position);

            // Limitar el zoom basado en la distancia m�nima y m�xima
            if (distanceToTarget > minZoomDistance && distanceToTarget < maxZoomDistance)
            {
                transform.position = newPosition;
            }
        }

        // Movimiento de la c�mara con el click derecho basado en la direcci�n de la c�mara
        if (Input.GetMouseButtonDown(1))
        {
            isManualControl = true;
            manualControlTimer = manualControlDuration;
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);

            // Mover la c�mara seg�n la direcci�n a la que mira
            Vector3 move = transform.right * difference.x * moveSpeed + transform.up * difference.y * moveSpeed;
            transform.Translate(move, Space.World);

            dragOrigin = Input.mousePosition;
        }

        // Rotaci�n de la c�mara con el click central
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
            // Solo actualiza la posici�n de la c�mara para seguir al objeto suavemente
            Vector3 targetPosition = target.position - transform.forward * initialDistanceFromTarget;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
