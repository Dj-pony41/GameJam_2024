using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    // Lista de balones para arrastrar desde el Inspector
    public List<GameObject> balls;
    // El bot�n correspondiente a cada bal�n
    public List<Button> ballButtons;

    // Variable para almacenar el �ndice del bal�n actualmente visible
    private int currentBallIndex = -1;

    void Start()
    {
        // Buscar el bal�n que est� activo al inicio y guardarlo
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].activeSelf)
            {
                currentBallIndex = i;
                break;
            }
        }

        // Si ning�n bal�n est� activo al inicio, hacemos visible el primer bal�n de la lista
        if (currentBallIndex == -1 && balls.Count > 0)
        {
            balls[0].SetActive(true);
            currentBallIndex = 0;
        }

        // Asignar el evento de cada bot�n para seleccionar el bal�n correspondiente
        for (int i = 0; i < ballButtons.Count; i++)
        {
            int index = i; // Necesario para evitar problemas de cierre sobre la variable i en el lambda
            ballButtons[i].onClick.AddListener(() => SelectBall(index));
        }
    }

    // Funci�n para seleccionar un bal�n basado en el �ndice
    void SelectBall(int index)
    {
        if (index != currentBallIndex)
        {
            // Obtener la posici�n del bal�n actualmente visible
            Vector3 currentPosition = balls[currentBallIndex].transform.position;

            // Ocultar el bal�n actual
            balls[currentBallIndex].SetActive(false);

            // Colocar el nuevo bal�n en la misma posici�n que el anterior
            balls[index].transform.position = currentPosition;

            // Activar el nuevo bal�n
            balls[index].SetActive(true);

            // Actualizar el �ndice del bal�n actual
            currentBallIndex = index;
        }
    }
}