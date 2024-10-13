using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final_mapa : MonoBehaviour
{
    public string nextSceneName;  // Nombre de la siguiente escena a cargar

    // Este m�todo se llama cuando la pelota entra en contacto con un Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que choc� es el punto de meta
        if (other.gameObject.CompareTag("Goal"))  // Usar la etiqueta "Goal" para identificar el punto de meta
        {
            Debug.Log("�Has ganado! Cambiando a la siguiente escena...");
            LoadNextLevel();  // Llamar al m�todo para cargar la siguiente escena
        }
    }

    // M�todo para cargar la siguiente escena
    void LoadNextLevel()
    {
        // Cargar la escena especificada en el Inspector
        SceneManager.LoadScene(nextSceneName);
    }
}
