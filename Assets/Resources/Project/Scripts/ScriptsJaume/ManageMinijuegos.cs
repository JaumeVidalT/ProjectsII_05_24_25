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
        DesactivarTodosLosMinijuegos();
    }

    private void Update()
    {
        IniciarMinijuego(2);
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
                ManageSalas.Instance.GetSalaActual().DestroyEvento();
                ManageSalas.Instance.SetMinijuegoActivo(false);
                GameObject.Find("Player").GetComponent<CharacterMovement>().enabled = true;
                
            }
        }
        else
        {
            Debug.LogWarning("�ndice fuera de rango.");
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
    public void StartMinijuego(Salas salaActual)
    {
        if(salaActual.eventoInstanciado != null)
        {
            switch (salaActual.eventoInstanciado.getTypeOfProblem())
            {
                case Evento.TypeOfProblem.FIRE:
                    ManageMinijuegos.Instance.IniciarMinijuego(0);
                    break;
                case Evento.TypeOfProblem.SHORTCIRCUIT:
                    ManageMinijuegos.Instance.IniciarMinijuego(1);
                    break;
                case Evento.TypeOfProblem.GASLEAK:
                    ManageMinijuegos.Instance.IniciarMinijuego(2);
                    break;
            }
        }
        
        if(salaActual.GetterEventoSala().getTypeOfProblem()==Evento.TypeOfProblem.NADA&& salaActual.GetypeOfSala() != Salas.typeOfSala.NONE)
        {
            ManageMinijuegos.Instance.IniciarMinijuego(0);
        }
    }


}