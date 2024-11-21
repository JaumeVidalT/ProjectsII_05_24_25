using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ManageSalas : MonoBehaviour
{
    public static ManageSalas Instance { get; private set; }
    private int tiempo;
    // Start is called before the first frame update
    public Transform[] rooms; 
    private List<Salas> salasList = new List<Salas>();
    private Salas salaActual;
    private Salas salaNext;
    [SerializeField]
    private GameObject Player;
    private GameObject currentPlayerInstance;
    public GameObject nextSala;
    private bool minijuegoActivo = false;
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
            salaActual = salaNext;
            PrintPlayer();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            tiempo++;
            if (salaActual.GetEventoEnSala())
            {
                ManageMinijuegos.Instance.StartMinijuego(salaActual);
                minijuegoActivo = true;
            }

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
        if (minijuegoActivo == true) 
        {
            ManageMinijuegos.Instance.StartMinijuego(salaActual);
        }
        

    }
    public Salas GetSalaActual() { return salaActual; }
    public void SetMinijuegoActivo(bool seter) {  minijuegoActivo=seter; }
    public void MoverJugadorSalas()
    {
        Salas nuevaSala = null;
        switch (Input.inputString.ToLower()) // Convertir a min�scula para evitar problemas de may�sculas
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
            salaNext = nuevaSala;
        }
    }
    
    public void PrintPlayer()
    {
        if (currentPlayerInstance == null)
        {
            // Si no existe un jugador en la escena, inst�ncialo
            currentPlayerInstance = Instantiate(Player, salaActual.transform.position, Quaternion.identity);
        }
        else
        {
            // Si el jugador ya existe, simplemente actualiza su posici�n
            currentPlayerInstance.transform.position = salaActual.transform.position;
        }
    }
    public void PrintNextSala()
    {
        if (nextSala == null)
        {
            // Si no existe un jugador en la escena, inst�ncialo
            nextSala = Instantiate(nextSala, salaNext.transform.position, Quaternion.identity);
        }
        else
        {
            // Si el jugador ya existe, simplemente actualiza su posici�n
            nextSala.transform.position = salaNext.transform.position;
        }
    }



}
