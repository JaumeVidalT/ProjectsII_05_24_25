using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProblem : MonoBehaviour
{
    enum TypeOfProblem { 
        FIRE,
        SHORTCIRCUIT,
        GASLEAK
    }
    
    public int dificulty;
    TypeOfProblem myTypeOfProblem;

    void Start()
    {
        dificulty = Random.Range(7, 15);
        myTypeOfProblem = (TypeOfProblem)Random.Range(0,3);

        Debug.Log("Problema de dificultad: " + dificulty + " de tipo " + myTypeOfProblem);
    }

}
