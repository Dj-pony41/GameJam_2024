using UnityEngine;
using UnityEngine.UI;

public class ControlBrillo : MonoBehaviour
{
    public Slider brilloSlider;  // El slider de brillo en la UI

    void Start()
    {
        // Configura los valores del slider (puedes ajustarlos según sea necesario)
        brilloSlider.minValue = 0f;    // Valor mínimo
        brilloSlider.maxValue = 100f;  // Valor máximo (puedes modificarlo si es necesario)
        brilloSlider.value = 50f;      // Valor inicial (por ejemplo, 50)

        // Escucha los cambios en el slider
        brilloSlider.onValueChanged.AddListener(EnviarValorDelSlider);
    }

    // Método para enviar el valor del slider como un número entero
    public void EnviarValorDelSlider(float valor)
    {
        // Convertir el valor flotante a un entero
        int valorEntero = Mathf.RoundToInt(valor);

        // Enviar o usar el valor entero aquí (puedes usar un método para enviar los datos)
        Debug.Log("Valor del slider: " + valorEntero);

        // Aquí puedes agregar la lógica para enviar el número a otra parte
    }
}
