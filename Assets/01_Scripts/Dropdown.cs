using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dropdown : MonoBehaviour
{
    public TMP_Dropdown dropdownResolucion; // Asigna el TMP_Dropdown en el Inspector

    // Lista para almacenar las resoluciones disponibles del sistema
    private List<Resolution> resoluciones = new List<Resolution>();

    void Start()
    {
        // Verificar que el TMP_Dropdown esté correctamente asignado
        if (dropdownResolucion == null)
        {
            Debug.LogError("El TMP_Dropdown no está asignado en el Inspector.");
            return;
        }

        // Llenar el Dropdown con las resoluciones disponibles de la pantalla
        LlenarOpcionesDropdown();

        // Añadir un listener para el evento onValueChanged del Dropdown
        dropdownResolucion.onValueChanged.AddListener(delegate { CambiarResolucion(dropdownResolucion.value); });
    }

    // Método para llenar el Dropdown con las resoluciones disponibles de la pantalla
    void LlenarOpcionesDropdown()
    {
        dropdownResolucion.ClearOptions(); // Limpiar opciones previas

        // Obtener las resoluciones disponibles del sistema
        Resolution[] resolucionesSistema = Screen.resolutions;

        List<string> opciones = new List<string>(); // Crear lista para opciones visibles

        // Agregar resoluciones a la lista de resoluciones
        foreach (var res in resolucionesSistema)
        {
            string opcion = res.width + "x" + res.height;
            opciones.Add(opcion);
            resoluciones.Add(res); // Almacenar la resolución en la lista
        }

        // Añadir opciones al Dropdown
        dropdownResolucion.AddOptions(opciones);

        // Seleccionar la resolución actual de la pantalla como opción predeterminada
        Resolution resolucionActual = Screen.currentResolution;
        int indiceResolucionActual = resoluciones.FindIndex(r => r.width == resolucionActual.width && r.height == resolucionActual.height);
        dropdownResolucion.value = indiceResolucionActual;
        dropdownResolucion.RefreshShownValue(); // Refrescar el valor mostrado
    }

    // Método para cambiar la resolución según la opción seleccionada en el Dropdown
    public void CambiarResolucion(int indice)
    {
        Resolution resolucionSeleccionada = resoluciones[indice]; // Obtener la resolución seleccionada
        Screen.SetResolution(resolucionSeleccionada.width, resolucionSeleccionada.height, FullScreenMode.Windowed); // Cambiar la resolución
        Debug.Log("Resolución cambiada a: " + resolucionSeleccionada.width + "x" + resolucionSeleccionada.height);
    }
}
