using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerTextWint;
    public TMP_Text timerText; // Texto para mostrar el tiempo usando TextMeshPro
    public float countdownTime = 60f; // Tiempo inicial en segundos (1 minuto)
    private float countdownWin = 300f;
    private float currentTime;

    void Start()
    {
        ResetTimer(); // Inicializar el temporizador al inicio
    }

    void Update()
    {
        // Reducir el tiempo en función del tiempo transcurrido
        currentTime -= Time.deltaTime;
        countdownWin-=Time.deltaTime;

        // Formatear el tiempo en minutos y segundos, y actualizar el texto del temporizador
        timerText.text = FormatTime(currentTime);
        timerTextWint.text = FormatTime(countdownWin);

        // Si el tiempo llega a 0, reiniciar el temporizador
        if (currentTime <= 0)
        {
            ActualizadorDeMedidores.instance.Actualizar();
            ManageSalas.Instance.CreateProblems();
            ResetTimer();
        }
    }

    // Método para reiniciar el temporizador
    void ResetTimer()
    {
        currentTime = countdownTime; // Reiniciar el tiempo
    }

    // Método para formatear el tiempo en minutos y segundos
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60); // Calcular los minutos
        int seconds = Mathf.FloorToInt(time % 60); // Calcular los segundos sobrantes
        return string.Format("{0:00}:{1:00}", minutes, seconds); // Retornar en formato MM:SS
    }
}
