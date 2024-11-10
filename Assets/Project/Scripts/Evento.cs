using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Evento : MonoBehaviour
{
    protected float ActualStatus;
    public GameObject problem;  
   

    public TextMeshPro mostradorDeDificultad;

    public void ActualizarTexto(int restador, Salas sala)
    {
        sala.modifyDificulty(restador);
        mostradorDeDificultad.text = sala.getDificulty(sala).ToString();
    }

    public void ActualizarSala(Salas sala)  // Cambié el nombre del método
    {
        sala.modifyDificulty(Random.Range(7, 15)) ;
        sala.setTypeOfProblem(Random.Range(0, 3));

        ActualizarTexto(sala.getDificulty(sala), sala);  // Pasamos el valor de dificultad correcto
        Instantiate(problem, sala.transform.position, Quaternion.identity);

        Debug.Log("Creado problema de dificultad: " + sala.getDificulty(sala) + " de tipo " + sala.getTypeOfProblem());
    }
}