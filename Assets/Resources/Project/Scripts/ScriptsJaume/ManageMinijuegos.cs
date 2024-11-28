using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMinijuegos : MonoBehaviour
{
    public static ManageMinijuegos Instance { get; private set; }

    public List<Minijuego> minijuegos;

    private void Awake()
    {
        // Asegura que solo haya una instancia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); //Assgura que persista entre escenes
    }

    private void Start()
    {
        // Primero asegurémonos de que todos los objetos Minijuego estén activados para que sean encontrados.
        GameObject[] allMinijuegos = GameObject.FindGameObjectsWithTag("Minijuego");

        minijuegos = new List<Minijuego>();

        foreach (var obj in allMinijuegos)
        {
            Minijuego minijuego = obj.GetComponent<Minijuego>();
            if (minijuego != null)
            {
                minijuegos.Add(minijuego);
            }
        }
        DesactivarTodosLosMinijuegos();
    }

    public void IniciarMinijuego(int index)
    {
        if (index >= 0 && index < minijuegos.Count)
        {
            if (!minijuegos[index].VerificarJuegoCompletado())
            {
                minijuegos[index].gameObject.SetActive(true);
                minijuegos[index].IniciarMinijuego();
            }
            else
            {
                minijuegos[index].gameObject.SetActive(false);
                minijuegos[index].TerminarMinijuego();
            }
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango.");
        }
    }
    public void DesactivarTodosLosMinijuegos()
    {
        foreach (var minijuego in minijuegos)
        {
            minijuego.gameObject.SetActive(false);
        }
        Debug.Log("Todos los mini-juegos han sido desactivados.");
    }


}