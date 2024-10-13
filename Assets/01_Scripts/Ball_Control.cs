using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Control : MonoBehaviour
{
    public Vector3 assignedPosition;  // Posici�n asignada a la que el objeto vuelve al presionar "R"
    private Vector3 lastPosition;     // �ltima posici�n registrada del objeto
    private List<Vector3> positionHistory = new List<Vector3>();  // Lista para guardar las �ltimas posiciones
    private float timeStationary = 0f; // Tiempo que el objeto ha estado quieto
    public float stationaryThreshold = 1f;  // Tiempo m�nimo para considerar que el objeto est� quieto
    public Collider boundaryCollider;  // El Collider que define los l�mites

    private Rigidbody rb;  // Referencia al Rigidbody del objeto

    private void Start()
    {
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody>();  // Obtiene el Rigidbody del objeto
    }

    void Update()
    {
        // Verifica si el objeto est� quieto
        if (transform.position == lastPosition)
        {
            timeStationary += Time.deltaTime;

            // Si el objeto ha estado quieto por menos de un segundo, guarda la posici�n en la lista
            if (timeStationary < stationaryThreshold && timeStationary > 0.1f)
            {
                positionHistory.Add(transform.position);
            }
        }
        else
        {
            timeStationary = 0f;  // Reinicia el contador si el objeto se ha movido
        }

        lastPosition = transform.position;  // Actualiza la �ltima posici�n registrada

        // Si se presiona la tecla "R", mueve el objeto a la posici�n asignada
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = assignedPosition;
            StartCoroutine(FreezeRigidbodyForAMillisecond());
        }

        // Si se presiona la tecla "E", mueve el objeto a la �ltima posici�n guardada en la lista y congela su Rigidbody por un milisegundo
        if (Input.GetKeyDown(KeyCode.E) && positionHistory.Count > 0)
        {
            MoveToLastPosition();
        }
    }

    // Corrutina que congela el Rigidbody por un milisegundo
    private IEnumerator FreezeRigidbodyForAMillisecond()
    {
        // Congela la posici�n y rotaci�n del Rigidbody
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        // Espera un milisegundo (0.001 segundos)
        yield return new WaitForSeconds(0.001f);

        // Descongela todas las restricciones del Rigidbody (o puedes ajustar las restricciones que desees)
        rb.constraints = RigidbodyConstraints.None;
    }

    // Mueve el objeto a la �ltima posici�n guardada
    private void MoveToLastPosition()
    {
        transform.position = positionHistory[positionHistory.Count - 1];
        positionHistory.RemoveAt(positionHistory.Count - 1);  // Elimina la �ltima posici�n de la lista
        StartCoroutine(FreezeRigidbodyForAMillisecond());     // Congela el Rigidbody por un milisegundo
    }

    // Detecta cuando el objeto sale del Collider
    private void OnTriggerExit(Collider other)
    {
        if (other == boundaryCollider && positionHistory.Count > 0)
        {
            MoveToLastPosition();  // Vuelve a la �ltima posici�n guardada al salir del Collider
        }
    }
}
