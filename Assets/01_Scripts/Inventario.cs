using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    // Lista de todos los GameObjects que ser�n gestionados por este script
    public List<GameObject> gameObjects; // Arrastra los GameObjects desde el Inspector

    // El bot�n que controlar� el GameObject asignado
    private Button boton;

    // El �ndice del GameObject en la lista que se debe mostrar al hacer clic
    public int indiceAMostrar;

    // Se ejecuta al inicio
    void Start()
    {
        // Asignar el bot�n a la variable
        boton = GetComponent<Button>();

        // Asegurarse de que el bot�n tenga un listener asignado
        if (boton != null)
        {
            // Cuando se hace clic en el bot�n, ejecuta el m�todo "MostrarOcultarGameObject"
            boton.onClick.AddListener(MostrarOcultarGameObject);
        }

        // Inicialmente, solo mostrar el GameObject asignado a este bot�n y ocultar los dem�s
        ActualizarVisibilidad();
    }

    // M�todo que se ejecutar� al hacer clic en el bot�n
    void MostrarOcultarGameObject()
    {
        // Llamar a la funci�n para actualizar la visibilidad de los objetos
        ActualizarVisibilidad();
    }

    // Actualiza la visibilidad de todos los GameObjects de la lista
    void ActualizarVisibilidad()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i] != null)
            {
                // Solo el GameObject en el �ndice indicado se mostrar�, los dem�s se ocultar�n
                gameObjects[i].SetActive(i == indiceAMostrar);
            }
        }
    }
}
