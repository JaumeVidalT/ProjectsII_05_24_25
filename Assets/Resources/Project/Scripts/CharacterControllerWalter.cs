using TMPro;
using UnityEngine;



public class CharacterControllerWalter : MonoBehaviour
{
    public float velocity;
    float movementX, movementY;
    
    int dadosLanzables = 4;
    int dadosSeleccionados = 1;
    private Manager_Dados managerDados;
    RandomProblem problem = null;
    RandomProblem bloqueo = null;
    GameObject salaActual = null;
    bool dentroDeSala = false;
    //int valorParaBloqueo = 0;
    private bool dadosMostrados = false;
    const int recuperarTurnos = 5;
    const int recuperarDados = 4;

    int contadorDeTurnos = 0;
    int contadorDeTurnosExtra = recuperarTurnos;

    public Canvas CanvaDados;
    public TextMeshProUGUI indicadorDeDados;
    public TextMeshProUGUI indicadorDeTurnos;
    public TextMeshProUGUI IndicadorDadosSeleccionados;
    public TextMeshProUGUI ConfirmarResultado;

    static CharacterControllerWalter instance = null;
    void Start()
    {
        instance = this;
        managerDados = FindObjectOfType<Manager_Dados>(); // Automatically finds Manager_Dados in the scene
        if (managerDados == null)
        {
            Debug.LogError("No se encontr� el componente Manager_Dados en la escena.");
        }
        ActualizarDados();
        indicadorDeTurnos.text = "Turnos restantes: " + (contadorDeTurnosExtra - contadorDeTurnos);
    }

    void SiguienteTurno()
    {
        contadorDeTurnos++;

        if (contadorDeTurnos >= contadorDeTurnosExtra)
        {
            RandomRoomSelecter.creadorDeProblemas.CreateProblemInRandomRoom();
            SeleccionadorDeBloquosAleatorios.instancia.CrearBloqueos();
            contadorDeTurnosExtra = contadorDeTurnos + recuperarTurnos;
            dadosLanzables = recuperarDados;
            managerDados.Restart(dadosLanzables+1);
            ActualizarDados();
        }

        indicadorDeTurnos.text = "Turnos restantes: " + (contadorDeTurnosExtra - contadorDeTurnos);
    }

    void ActualizarDados()
    {
        indicadorDeDados.text = "Dados: " + dadosLanzables;
        IndicadorDadosSeleccionados.text = "Dados selecionados: " + dadosSeleccionados;
    }

    void Update()
    {
        movementX = Input.GetAxis("Horizontal") * velocity;
        movementY = Input.GetAxis("Vertical") * velocity;

        Vector3 position = transform.position;
        GetComponent<Rigidbody2D>().MovePosition(new Vector3(movementX + position.x, movementY + position.y, position.z));

        // Lanzar dados con la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!dadosMostrados) // Solo lanza dados si aún no han sido lanzados en este turno
            {
                /*CanvaDados.gameObject.SetActive(true);*/
                ConfirmarResultado.text = "Espacio para confirmar Resultado";
                LanzarDados();
            }
            else  // Si los dados ya fueron lanzados, borra los dados y pasa al siguiente turno
            {
                /*CanvaDados.gameObject.SetActive(false);*/
                BorrarDadosYReiniciarTurno();
                
            }
        }

        // Aumentar la selección de dados con la tecla Z
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (dadosSeleccionados < managerDados.Dado2.Count)
            {
                // Asegura que no seleccione más dados que el tamaño del array
                dadosSeleccionados++;
                ActualizarDados();
            }
        }
    }

    void LanzarDados()
    {
        int finalDiceSum = 0;

        for (int i = 0; i < dadosSeleccionados; i++)
        {
            int dadoActual = Random.Range(1, 6);
            Debug.Log(dadoActual);
            managerDados.PrintDado(dadoActual, i);
            finalDiceSum += dadoActual;
        }

        dadosMostrados = true;

        // Realiza la lógica de verificación de sala y problemas
        if ((salaActual != null && (salaActual.gameObject.name == "salaOxigeno"
            || salaActual.gameObject.name == "salaTemp"
            || salaActual.gameObject.name == "salaPresion"))
            && dentroDeSala)
        {
            salaActual.GetComponent<Reseteador>().Resetear();
            RandomRoomSelecter.creadorDeProblemas.RestarLosMedidores();
        }

        // Verifica los problemas o bloqueos y actualiza el estado
        VerificarProblemasOBloqueos(finalDiceSum);
    }

    void BorrarDadosYReiniciarTurno()
    {
        // Desactiva el último dado del array en managerDados y reduce el contador de dados lanzables
        if (dadosLanzables > 0)
        {
            // Desactiva el último dado
            

            // Reduce el contador de dados disponibles
            if(dadosSeleccionados>1)
            {
                managerDados.BorrarDado(dadosLanzables-1);
                dadosLanzables--;
            }
            

            // Actualiza la visualización de dados
            ActualizarDados();
        }
        
        SiguienteTurno();
        dadosSeleccionados = 1;
        dadosMostrados = false;
    }

    void VerificarProblemasOBloqueos(int finalDiceSum)
    {
        if (problem)
        {
            if (finalDiceSum >= problem.dificulty)
            {
                problem.gameObject.SetActive(false);
                problem = null;
            }
            else
            {
                problem.ActualizarTexto(finalDiceSum);
            }


            RandomRoomSelecter.creadorDeProblemas.RestarLosMedidores();
        }
        else if (bloqueo)
        {
            if (finalDiceSum >= bloqueo.dificulty)
            {
                bloqueo.gameObject.SetActive(false);
                bloqueo = null;
            }
            else
            {
                bloqueo.ActualizarTexto(finalDiceSum);
            }


            RandomRoomSelecter.creadorDeProblemas.RestarLosMedidores();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Habitacion")
        {
            if (salaActual != collision.gameObject)
            {
                salaActual = collision.gameObject;
                SiguienteTurno();
                RandomRoomSelecter.creadorDeProblemas.RestarLosMedidores();
            }

            dentroDeSala = true;
        }
        else if (collision.tag == "Finish")
        {
            problem = collision.GetComponent<RandomProblem>();
        }
        else if (collision.tag == "Respawn")
        {
            bloqueo = collision.GetComponent<RandomProblem>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            problem = null;
        }
        else if (collision.tag == "Respawn")
        {
            bloqueo = null;
        }
        else if (collision.tag == "Habitacion")
        {
            dentroDeSala = true;
        }

    }
}
