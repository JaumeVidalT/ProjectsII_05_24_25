using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSalas : MonoBehaviour
{
    private int tiempo;
    // Start is called before the first frame update
    public Transform[] rooms; 
    private List<Salas> salasList = new List<Salas>();
    private Salas salaActual;
    [SerializeField]
    private GameObject Player;
    private GameObject currentPlayerInstance;
    public GameObject nextSala;
    public void Start()
    {
        foreach (var room in rooms)
        {
            Salas sala = room.GetComponent<Salas>(); // Asume que cada `Transform` tiene el componente `Salas`
            if (sala != null)
            {
                salasList.Add(sala);
                Debug.Log("Agregada sala: ");
            }
            else
            {
                Debug.LogWarning("Sala en salasList es null!");
            }
        }
        salasList[0].salaDerecha = salasList[1];
        salasList[1].salaIzquierda = salasList[0];
        salaActual = salasList[0];
        PrintPlayer();
    }
    // Update is called once per frame
    public void Update()
    {
        MoverJugadorSalas();
        PrintNextSala();
        if(Input.GetKeyUp(KeyCode.I))
        {
            PrintPlayer();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            tiempo++;
        }
        if(tiempo>5)
        {
            foreach (Salas sala in salasList)
            {
                if (Random.Range(0, 100) > 30&& !sala.GetEventoEnSala())
                {
                    Evento evento = sala.GetComponent<Evento>();
                    sala.UpdateSala();                  
                    
                }

            }
            tiempo = 0;
        }
        
    }
    public void MoverJugadorSalas()
    {
        Salas nuevaSala = null;
        switch (Input.inputString.ToLower()) // Convertir a minúscula para evitar problemas de mayúsculas
        {            
            case "w":
                nuevaSala = salaActual.salaArriba;
                break;
            case "s":
                nuevaSala = salaActual.salaAbajo;
                break;
            case "a":
                nuevaSala = salaActual.salaIzquierda;
                break;
            case "d":
                nuevaSala = salaActual.salaDerecha;
                break;
            default:
                // No se hace nada si no se presiona W, A, S o D
                break;
        }
        if(nuevaSala!=null)
        {
            salaActual = nuevaSala;
        }
    }
    public void PrintPlayer()
    {
        if (currentPlayerInstance == null)
        {
            // Si no existe un jugador en la escena, instáncialo
            currentPlayerInstance = Instantiate(Player, salaActual.transform.position, Quaternion.identity);
        }
        else
        {
            // Si el jugador ya existe, simplemente actualiza su posición
            currentPlayerInstance.transform.position = salaActual.transform.position;
        }
    }
    public void PrintNextSala()
    {
        if (nextSala == null)
        {
            // Si no existe un jugador en la escena, instáncialo
            nextSala = Instantiate(nextSala, salaActual.transform.position, Quaternion.identity);
        }
        else
        {
            // Si el jugador ya existe, simplemente actualiza su posición
            nextSala.transform.position = salaActual.transform.position;
        }
    }



}
