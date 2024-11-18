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
        // Primero asegur�monos de que todos los objetos Minijuego est�n activados para que sean encontrados.
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
    }
    void Update()
    {
<<<<<<< HEAD
            IniciarMinijuego(2);
        
                                  
=======
        IniciarMinijuego(0);
>>>>>>> parent of 57bbbec (Feature Gas Terminada)
    }
    public void IniciarMinijuego(int index)
    {
        if (index >= 0 && index < minijuegos.Count)
        {
            if (!minijuegos[index].VerificarJuegoCompletado())
            {
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
            Debug.LogWarning("�ndice fuera de rango.");
        }
    }


}