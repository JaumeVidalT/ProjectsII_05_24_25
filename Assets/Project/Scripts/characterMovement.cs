using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float velocity;
    float movementX,movementY;

    int howManyDice = 4;
    RandomProblem problem = null;

    void Start()
    {
       
    }

    void Update()
    {
        movementX = Input.GetAxis("Horizontal")* velocity;
        movementY = Input.GetAxis("Vertical")* velocity;

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

            if (finalDiceSum >= problem.dificulty && problem != null)
            {
                Debug.Log("Problema de dificultad " + problem.dificulty + " resuelto con un " + finalDiceSum);
                Destroy(problem.gameObject);
                problem = null;

                RandomRoomSelecter.creadorDeProblemas.CreateProblemInRandomRoom();
            }
            else if (problem != null) {
                Debug.Log("Problema de dificultad " + problem.dificulty + " NO resuelto con un " + finalDiceSum);
                Debug.Log("Problema reducido a " + (problem.dificulty - finalDiceSum));

                problem.dificulty -= finalDiceSum;
            }
            if (howManyDice > 1)
                howManyDice--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish") {
            problem = collision.GetComponent<RandomProblem>();
        }
    }
}
