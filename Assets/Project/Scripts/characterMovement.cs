using UnityEngine;


public class characterMovement : MonoBehaviour
{
    public float velocity;
    float movementX, movementY;

    int howManyDice = 4;
    RandomProblem problem = null;
    GameObject bloqueo = null;
    int valorParaBloqueo = 0;


    int contadorDeTurnos = 0;
    int contadorDeTurnosExtra = 3;


    void Start()
    {

    }

    void SiguienteTurno()
    {
        contadorDeTurnos++;
        
        if (contadorDeTurnos >= contadorDeTurnosExtra)
        {
            RandomRoomSelecter.creadorDeProblemas.CreateProblemInRandomRoom();
            SeleccionadorDeBloquosAleatorios.instancia.CrearBloqueos();
            contadorDeTurnosExtra = contadorDeTurnos + 3;
            howManyDice = 4;
        }
    }

    void Update()
    {
        movementX = Input.GetAxis("Horizontal") * velocity;
        movementY = Input.GetAxis("Vertical") * velocity;

        Vector3 position = transform.position;

        GetComponent<Rigidbody2D>().MovePosition(new Vector3(movementX + position.x, movementY + position.y, position.z));


        //lanzar dados con espacio y comprovar si el 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int finalDiceSum = 0;

            for (int i = 0; i < howManyDice; i++)
            {
                finalDiceSum += Random.Range(1, 6);
            }

            if (problem)
            {
                if (finalDiceSum >= problem.dificulty)
                {
                    Debug.Log("Problema de dificultad " + problem.dificulty + " resuelto con un " + finalDiceSum);
                    Destroy(problem.gameObject);
                    problem = null;
                }
                else
                {

                    Debug.Log("Problema de dificultad " + problem.dificulty + " NO resuelto con un " + finalDiceSum);
                    Debug.Log("Problema reducido a " + (problem.dificulty - finalDiceSum));

                    //problem.dificulty -= finalDiceSum;
                    problem.ActualizarTexto(finalDiceSum);
                }
            }
            else if (bloqueo)
            {

                if (finalDiceSum >= valorParaBloqueo)
                {
                    Destroy(bloqueo);
                }
                else
                {
                    valorParaBloqueo -= finalDiceSum;
                    Debug.Log("Bloqueo reducido a " + valorParaBloqueo);
                }
            }


            if (howManyDice > 1)
                howManyDice--;

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
            bloqueo = collision.gameObject;
            valorParaBloqueo = Random.Range(7, 12);
        }
    }
}
