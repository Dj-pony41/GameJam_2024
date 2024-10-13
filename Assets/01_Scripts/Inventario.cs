using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    // Lista de todos los GameObjects que serán gestionados por este script
    public List<GameObject> gameObjects; // Arrastra los GameObjects desde el Inspector

    // El botón que controlará el GameObject asignado
    private Button boton;

    // El índice del GameObject en la lista que se debe mostrar al hacer clic
    public int indiceAMostrar;

    // Se ejecuta al inicio
    void Start()
    {
        // Asignar el botón a la variable
        boton = GetComponent<Button>();

        // Asegurarse de que el botón tenga un listener asignado
        if (boton != null)
        {
            // Cuando se hace clic en el botón, ejecuta el método "MostrarOcultarGameObject"
            boton.onClick.AddListener(MostrarOcultarGameObject);
        }

        // Inicialmente, solo mostrar el GameObject asignado a este botón y ocultar los demás
        ActualizarVisibilidad();
    }

    // Método que se ejecutará al hacer clic en el botón
    void MostrarOcultarGameObject()
    {
        // Llamar a la función para actualizar la visibilidad de los objetos
        ActualizarVisibilidad();
    }

    // Actualiza la visibilidad de todos los GameObjects de la lista
    void ActualizarVisibilidad()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i] != null)
            {
                // Solo el GameObject en el índice indicado se mostrará, los demás se ocultarán
                gameObjects[i].SetActive(i == indiceAMostrar);
            }
        }
    }
}
