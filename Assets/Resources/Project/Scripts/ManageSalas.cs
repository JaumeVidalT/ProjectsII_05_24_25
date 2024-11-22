
using System.Collections.Generic;
using UnityEngine;
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
    private Salas nuevaSala = null;
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
        salasList[0].salaAbajo = salasList[2];
        salasList[2].salaArriba = salasList[0];
        salasList[3].salaArriba = salasList[1];
        salasList[1].salaAbajo = salasList[3];
        salasList[3].salaIzquierda = salasList[2];
        salasList[2].salaDerecha = salasList[3];
        salaActual = salasList[0];
        salaNext = salaActual;
        CreateProblems();
        PrintPlayer();
    }
    // Update is called once per frame
    public void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.E))
        {
            

            if (salaActual.GetEventoEnSala())
            {
                ManageMinijuegos.Instance.StartMinijuego(salaActual);
                minijuegoActivo = true;
            }

        }

        if (minijuegoActivo == true) 
        {
            ManageMinijuegos.Instance.StartMinijuego(salaActual);
        }
        

    }
    public void CreateProblems()
    {
        foreach (Salas sala in salasList)
        {
            if (Random.Range(0, 100) > 30 && !sala.GetEventoEnSala())
            {
                Evento evento = sala.GetComponent<Evento>();
                sala.UpdateSala();

            }

        }
    }
    public Salas GetSalaActual() { return salaActual; }
    public void SetMinijuegoActivo(bool seter) {  minijuegoActivo=seter; }

    
    public void PrintPlayer()
    {
        if (currentPlayerInstance == null)
        {
            // Si no existe un jugador en la escena, inst�ncialo
            currentPlayerInstance = Instantiate(Player, salaActual.transform.position, Quaternion.identity);
        }

    }
    public int ContarProblemasFuego()
    {
        int contadorDeProblemas=0;
        foreach (Salas sala in salasList)
        {
           if(sala.GetEventoEnSala()&&sala.eventoInstanciado.getTypeOfProblem()==Evento.TypeOfProblem.FIRE)
            {
                contadorDeProblemas++;
            }
        }
        return contadorDeProblemas;

    }
    public int ContarProblemasGas()
    {
        int contadorDeProblemas = 0;
        foreach (Salas sala in salasList)
        {
            if (sala.GetEventoEnSala() && sala.eventoInstanciado.getTypeOfProblem() == Evento.TypeOfProblem.GASLEAK)
            {
                contadorDeProblemas++;
            }
        }
        return contadorDeProblemas;

    }
    public int ContarProblemasElectricidad()
    {
        int contadorDeProblemas = 0;
        foreach (Salas sala in salasList)
        {
            if (sala.GetEventoEnSala() && sala.eventoInstanciado.getTypeOfProblem() == Evento.TypeOfProblem.SHORTCIRCUIT)
            {
                contadorDeProblemas++;
            }
        }
        return contadorDeProblemas;

    }


}
