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
    
    int dificulty;
    TypeOfProblem myTypeOfProblem;

    void Start()
    {
        dificulty = Random.Range(7, 15);
        myTypeOfProblem = (TypeOfProblem)Random.Range(0,3);

        Debug.Log("problem spawned with difficulty: " + dificulty + " for " + myTypeOfProblem);
    }

}
