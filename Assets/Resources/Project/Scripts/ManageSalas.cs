
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    private bool minijuegoActivo = false;
    private Salas nuevaSala = null;
    private bool OnObject=false;
    public Canvas canvas;


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
            }
            else
            {
                Debug.LogWarning("Sala en salasList es null!");
            }
        }
        salaActual = salasList[0];
        SetSalasReset();
        CreateProblems();
    }
    // Update is called once per frame
    public void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.E)&&OnObject==true)
        {
           
            if (salaActual.GetEventoEnSala())
            {
                canvas.gameObject.SetActive(false);
                GameObject.Find("Player").GetComponent<CharacterMovement>().enabled=false;
                ManageMinijuegos.Instance.StartMinijuego(salaActual);
                minijuegoActivo = true;
            }
            else if(salaActual.GetypeOfSala()!=Salas.typeOfSala.NONE)
            {
                GameObject.Find("Player").GetComponent<CharacterMovement>().enabled = false;
                ManageMinijuegos.Instance.StartMinijuego(salaActual);
                minijuegoActivo = true;
            }
            
           
        }

        if (minijuegoActivo == true) 
        {
            ManageMinijuegos.Instance.StartMinijuego(salaActual);
        }
        else
        {
            canvas.gameObject.SetActive(true);
        }
        

    }
    public void setOnObject(bool _OnObject)
    {
        OnObject= _OnObject;
    }
    public void CreateProblems()
    {
        foreach (Salas sala in salasList)
        {
            if (Random.Range(0, 100) > 30 && !sala.GetEventoEnSala()&&sala.GetypeOfSala()==Salas.typeOfSala.NONE)
            {
                Evento evento = sala.GetComponent<Evento>();
                sala.UpdateSala();

            }

        }
    }
    public Salas GetSalaActual() { return salaActual; }
    public void SetMinijuegoActivo(bool seter) {  minijuegoActivo=seter; }
    public void SetSalaActual(Salas NextSala)
    {
        salaActual = NextSala;
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
    private void SetSalasReset()
    {
        GameObject.Find("floorOxygen").GetComponent<Salas>().setTypeOfSala(1);
        GameObject.Find("floorTemperature").GetComponent<Salas>().setTypeOfSala(2);
        GameObject.Find("floorElectricity").GetComponent<Salas>().setTypeOfSala(3);
    }
}


