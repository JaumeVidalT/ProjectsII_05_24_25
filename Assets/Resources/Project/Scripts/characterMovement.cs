using TMPro;
using UnityEngine;


public class characterMovement : MonoBehaviour
{
    public float velocity;
    float movementX, movementY;
    private Manager_Dados managerDados;
    int dadosLanzables = 4;
    RandomProblem problem = null;
    RandomProblem bloqueo = null;
    int valorParaBloqueo = 0;

    const int elExtra = 4;

    int contadorDeTurnos = 0;
    int contadorDeTurnosExtra = elExtra;


    public TextMeshProUGUI indicadorDeDados;
    public TextMeshProUGUI indicadorDeTurnos;

    static characterMovement instance = null;
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
            contadorDeTurnosExtra = contadorDeTurnos + elExtra;
            dadosLanzables = 4;
            managerDados.Restart(5);
            ActualizarDados();
        }

        indicadorDeTurnos.text = "Turnos restantes: " + (contadorDeTurnosExtra - contadorDeTurnos);
    }

    void ActualizarDados() {
        indicadorDeDados.text = "Dados: " + dadosLanzables;
    }
    

    void Update()
    {
        movementX = Input.GetAxis("Horizontal") * velocity;
        movementY = Input.GetAxis("Vertical") * velocity;

        Vector3 position = transform.position;

        GetComponent<Rigidbody2D>().MovePosition(new Vector3(movementX + position.x, movementY + position.y, position.z));

        //lanzar dados con espacio y comprovar si la dificultad es menor igual o mayor al resultado
        if (Input.GetKeyDown(KeyCode.Space))
        {
            managerDados.BorrarDado(dadosLanzables);
            int finalDiceSum = 0;
            int rollDice;
            for (int i = 0; i < dadosLanzables; i++)
            {

                rollDice = Random.Range(1, 7);
                managerDados.PrintDado( rollDice, i);
                finalDiceSum += rollDice;
            }
            dadosLanzables--;

            if (problem)
            {
                if (finalDiceSum >= problem.dificulty)
                {
                    //Debug.Log("Problema de dificultad " + problem.dificulty + " resuelto con un " + finalDiceSum);
                    Destroy(problem.gameObject);
                    problem = null;
                }
                else
                {
                    //Debug.Log("Problema de dificultad " + problem.dificulty + " NO resuelto con un " + finalDiceSum);
                    //Debug.Log("Problema reducido a " + (problem.dificulty - finalDiceSum));

                    //problem.dificulty -= finalDiceSum;
                    problem.ActualizarTexto(finalDiceSum);
                }

                if (dadosLanzables > 1)
                    dadosLanzables--;
            }
            else if (bloqueo)
            {

                if (finalDiceSum >= valorParaBloqueo)
                {
                    //Debug.Log("Bloqueo de dificultad " + bloqueo.dificulty + " resuelto con un " + finalDiceSum);
                    Destroy(bloqueo.gameObject);
                    bloqueo = null;
                }
                else
                {
                    Debug.Log("Bloqueo de dificultad " + bloqueo.dificulty + " NO resuelto con un " + finalDiceSum);
                    Debug.Log("Bloqueo reducido a " + (bloqueo.dificulty - finalDiceSum));

                    bloqueo.ActualizarTexto(finalDiceSum);
                }

                if (dadosLanzables > 1)
                    dadosLanzables--;
            }


            

            ActualizarDados();
            SiguienteTurno();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Habitacion")
        {
            SiguienteTurno();
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
        
    }
}
