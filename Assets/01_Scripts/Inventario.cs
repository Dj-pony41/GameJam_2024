using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    // Lista de balones para arrastrar desde el Inspector
    public List<GameObject> balls;
    // El botón correspondiente a cada balón
    public List<Button> ballButtons;

    // Variable para almacenar el índice del balón actualmente visible
    private int currentBallIndex = -1;

    void Start()
    {
        // Buscar el balón que esté activo al inicio y guardarlo
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].activeSelf)
            {
                currentBallIndex = i;
                break;
            }
        }

        // Si ningún balón está activo al inicio, hacemos visible el primer balón de la lista
        if (currentBallIndex == -1 && balls.Count > 0)
        {
            balls[0].SetActive(true);
            currentBallIndex = 0;
        }

        // Asignar el evento de cada botón para seleccionar el balón correspondiente
        for (int i = 0; i < ballButtons.Count; i++)
        {
            int index = i; // Necesario para evitar problemas de cierre sobre la variable i en el lambda
            ballButtons[i].onClick.AddListener(() => SelectBall(index));
        }
    }

    // Función para seleccionar un balón basado en el índice
    void SelectBall(int index)
    {
        if (index != currentBallIndex)
        {
            // Obtener la posición del balón actualmente visible
            Vector3 currentPosition = balls[currentBallIndex].transform.position;

            // Ocultar el balón actual
            balls[currentBallIndex].SetActive(false);

            // Colocar el nuevo balón en la misma posición que el anterior
            balls[index].transform.position = currentPosition;

            // Activar el nuevo balón
            balls[index].SetActive(true);

            // Actualizar el índice del balón actual
            currentBallIndex = index;
        }
    }
}