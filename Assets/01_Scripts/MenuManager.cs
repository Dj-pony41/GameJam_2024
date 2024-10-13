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
    public GameObject panelOpciones; // Si tienes m�s paneles puedes agregarlos aqu�

    // M�todo para mostrar el panel de niveles al presionar "CargarJuego"
    public void MostrarPanelNiveles()
    {
        CambiarPanel(panelInicio, panelNiveles);
    }

    // M�todo para regresar al panel de inicio desde otros paneles
    public void RegresarAlInicio()
    {
        CambiarPanel(panelNiveles, panelInicio);
    }

    // M�todo para mostrar el panel de opciones al presionar "Opciones"
    public void MostrarPanelOpciones()
    {
        CambiarPanel(panelInicio, panelOpciones);
    }

    // M�todo para cerrar el panel de opciones y volver al panel de inicio
    public void CerrarPanelOpciones()
    {
        CambiarPanel(panelOpciones, panelInicio);
    }

    // M�todo gen�rico para cambiar entre paneles
    public void CambiarPanel(GameObject panelActual, GameObject panelNuevo)
    {
        // Desactiva el panel actual y activa el nuevo panel
        panelActual.SetActive(false);
        panelNuevo.SetActive(true);
    }

    // M�todo para salir del juego
    public void SalirDelJuego()
    {
        // Si estamos en el editor de Unity, detiene el modo "Play"
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        // Si estamos en una build, cierra la aplicaci�n
        Application.Quit();
#endif
    }

    // M�todo para cargar la escena del juego
    public void ScenaJuego()
    {
        SceneManager.LoadScene("Jorge_Test");
    }
}
