using UnityEngine;
using UnityEngine.UI;

public class ControlBrillo : MonoBehaviour
{
    public Slider brilloSlider;  // El slider de brillo en la UI

    void Start()
    {
        // Configura los valores del slider (puedes ajustarlos seg�n sea necesario)
        brilloSlider.minValue = 0f;    // Valor m�nimo
        brilloSlider.maxValue = 100f;  // Valor m�ximo (puedes modificarlo si es necesario)
        brilloSlider.value = 50f;      // Valor inicial (por ejemplo, 50)

        // Escucha los cambios en el slider
        brilloSlider.onValueChanged.AddListener(EnviarValorDelSlider);
    }

    // M�todo para enviar el valor del slider como un n�mero entero
    public void EnviarValorDelSlider(float valor)
    {
        // Convertir el valor flotante a un entero
        int valorEntero = Mathf.RoundToInt(valor);

        // Enviar o usar el valor entero aqu� (puedes usar un m�todo para enviar los datos)
        Debug.Log("Valor del slider: " + valorEntero);

        // Aqu� puedes agregar la l�gica para enviar el n�mero a otra parte
    }
}
