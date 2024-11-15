using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ViejoScriptWalterNOTOCAR : MonoBehaviour
{
    public float velocity;
    float movementX, movementY;

    int dadosLanzables = 4;
    int dadosSeleccionados = 1;

    RandomProblem problem = null;
    RandomProblem bloqueo = null;
    GameObject salaActual = null;
    bool dentroDeSala = false;
    //int valorParaBloqueo = 0;

    const int recuperarTurnos = 5;
    const int recuperarDados = 4;

    int contadorDeTurnos = 0;
    int contadorDeTurnosExtra = 8;


    public TextMeshProUGUI indicadorDeDados;
    public TextMeshProUGUI indicadorDeTurnos;
    public TextMeshProUGUI indicadorDeRescate;
    public Image filtro;
    public Image[] dadosLanzados;
    int[] dadosLanzadosResultados = new int[5];
    int finalDiceSum;

    public Image[] dadosLanzablesImagen;

    static ViejoScriptWalterNOTOCAR instance = null;
    void Start()
    {
        instance = this;
        ActualizarDados();
        indicadorDeTurnos.text = "Turnos para problemas: " + (contadorDeTurnosExtra - contadorDeTurnos);
        indicadorDeRescate.text = "Turnos hasta rescate: " + (20 - contadorDeTurnos);
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

        indicadorDeTurnos.text = "Turnos para problemas: " + (contadorDeTurnosExtra - contadorDeTurnos);
        indicadorDeRescate.text = "Turnos hasta rescate: " + (20 - contadorDeTurnos);

        ActualizadorDeMedidores.instance.TurnoVivo();
    }

    void ActualizarDados()
    {
        indicadorDeDados.text = "Dados extra: " + dadosLanzables;
    }

    void ActualizarDadosConResta() {
        indicadorDeDados.text = "Dados extra: " + dadosLanzables + " -" + (dadosSeleccionados -1);
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
            /*int finalDiceSum = 0;

            for (int i = 0; i < dadosSeleccionados; i++)
            {
                int dadoActual = Random.Range(1, 6);
                //Debug.Log("Dado " + i + " es: " + dadoActual);


                finalDiceSum += dadoActual;
            }*/

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
                ActivarFiltroYDados();                

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

                StartCoroutine(DesActivarFiltroYDadosDuranteSegundos());
            }
            else if (bloqueo)
            {
                ActivarFiltroYDados();

                if (finalDiceSum >= bloqueo.dificulty)
                {

                    bloqueo.gameObject.SetActive(false);
                    bloqueo = null;
                }
                else
                {
                    bloqueo.ActualizarTexto(finalDiceSum);
                }
                
                StartCoroutine(DesActivarFiltroYDadosDuranteSegundos());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (dadosLanzables >= 1)
            {
                dadosSeleccionados++;
                dadosLanzables--;

                

                ActualizarDados();
            }
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

    void ActivarFiltroYDados() {
        velocity = 0.0f;

        filtro.gameObject.SetActive(true);
        for (int i = 0; i < dadosSeleccionados; i++)
        {
            dadosLanzados[i].gameObject.SetActive(true);
            dadosLanzadosResultados[i] = Random.Range(1, 7);

            dadosLanzados[i].sprite = Resources.Load<Sprite>
                ($"Project/Sprites/Dado{dadosLanzadosResultados[i]}");

            finalDiceSum += dadosLanzadosResultados[i];
        }
    }

    IEnumerator DesActivarFiltroYDadosDuranteSegundos() {
        

        yield return new WaitForSeconds(2);

        filtro.gameObject.SetActive(false);
        for (int i = 0; i < dadosSeleccionados; i++)
        {
            dadosLanzados[i].gameObject.SetActive(false);
        }

        velocity = 0.2f;


        RandomRoomSelecter.creadorDeProblemas.RestarLosMedidores();

        dadosSeleccionados = 1;
        finalDiceSum = 0;

        ActualizarDados();
        SiguienteTurno();        
    }
}
