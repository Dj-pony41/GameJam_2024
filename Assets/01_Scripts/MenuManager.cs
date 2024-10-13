using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; // Necesario para detener el juego dentro del editor
#endif

public class MenuManager : MonoBehaviour
{
    // Referencias a los paneles
    public GameObject panelInicio;
    public GameObject panelNiveles;
    public GameObject panelOpciones; // Si tienes más paneles puedes agregarlos aquí

    // Método para mostrar el panel de niveles al presionar "CargarJuego"
    public void MostrarPanelNiveles()
    {
        CambiarPanel(panelInicio, panelNiveles);
    }

    // Método para regresar al panel de inicio desde otros paneles
    public void RegresarAlInicio()
    {
        CambiarPanel(panelNiveles, panelInicio);
    }

    // Método para mostrar el panel de opciones al presionar "Opciones"
    public void MostrarPanelOpciones()
    {
        CambiarPanel(panelInicio, panelOpciones);
    }

    // Método para cerrar el panel de opciones y volver al panel de inicio
    public void CerrarPanelOpciones()
    {
        CambiarPanel(panelOpciones, panelInicio);
    }

    // Método genérico para cambiar entre paneles
    public void CambiarPanel(GameObject panelActual, GameObject panelNuevo)
    {
        // Desactiva el panel actual y activa el nuevo panel
        panelActual.SetActive(false);
        panelNuevo.SetActive(true);
    }

    // Método para salir del juego
    public void SalirDelJuego()
    {
        // Si estamos en el editor de Unity, detiene el modo "Play"
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        // Si estamos en una build, cierra la aplicación
        Application.Quit();
#endif
    }

    // Método para cargar la escena del juego
    public void ScenaJuego()
    {
        SceneManager.LoadScene("Jorge_Test");
    }
}
