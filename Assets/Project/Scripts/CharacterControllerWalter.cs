using TMPro;
using UnityEngine;



public class CharacterControllerWalter : MonoBehaviour
{
    public float velocity;
    float movementX, movementY;

    int dadosLanzables = 12;
    int dadosSeleccionados = 1;

    RandomProblem problem = null;
    RandomProblem bloqueo = null;
    GameObject salaActual = null;
    bool dentroDeSala = false;
    //int valorParaBloqueo = 0;

    const int recuperarTurnos = 5;
    const int recuperarDados = 12;

    int contadorDeTurnos = 0;
    int contadorDeTurnosExtra = recuperarTurnos;


    public TextMeshProUGUI indicadorDeDados;
    public TextMeshProUGUI indicadorDeTurnos;

    static CharacterControllerWalter instance = null;
    void Start()
    {
        instance = this;
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
            ActualizarDados();
        }

        indicadorDeTurnos.text = "Turnos restantes: " + (contadorDeTurnosExtra - contadorDeTurnos);
    }

    void ActualizarDados()
    {
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
            int finalDiceSum = 0;

            for (int i = 0; i < dadosSeleccionados; i++)
            {
                int dadoActual = Random.Range(1, 6);
                //Debug.Log("Dado " + i + " es: " + dadoActual);


                finalDiceSum += dadoActual;
            }

            // cuidado, que puede resetear aún en el pasillo
            if ((salaActual.gameObject.name == "salaOxigeno"
            || salaActual.gameObject.name == "salaTemp"
            || salaActual.gameObject.name == "salaPresion")
            && dentroDeSala)
            {
                salaActual.GetComponent<Reseteador>().Resetear();
                RandomRoomSelecter.creadorDeProblemas.RestarLosMedidores();
                salaActual.GetComponent<Reseteador>().Resetear();
            }

            if (problem)
            {
                if (finalDiceSum >= problem.dificulty)
                {
                    //Destroy(problem.gameObject);
                    problem.gameObject.SetActive(false);
                    problem = null;
                }
                else
                {
                    problem.ActualizarTexto(finalDiceSum);
                }

                if (dadosSeleccionados > 1)
                    dadosLanzables -= (dadosSeleccionados - 1);

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

                if (dadosSeleccionados > 1)
                    dadosLanzables -= (dadosSeleccionados - 1);

                RandomRoomSelecter.creadorDeProblemas.RestarLosMedidores();
            }




            ActualizarDados();
            SiguienteTurno();

            dadosSeleccionados = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (dadosSeleccionados < dadosLanzables)
                dadosSeleccionados++;
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
