using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salas : MonoBehaviour
{
    // Start is called before the first frame update
    protected Evento EventoSala;
    protected bool enSala;
    protected int dificulty;

    public enum TypeOfProblem
    {
        FIRE,
        SHORTCIRCUIT,
        GASLEAK
    };

    protected TypeOfProblem myTypeOfProblem;
    // Update is called once per frame

    public void modifyDificulty(int amount)
    {
        dificulty = amount;
    }
    public void setTypeOfProblem(int amount)
    {
        myTypeOfProblem = (TypeOfProblem)amount;
    }
    public int getDificulty(Salas sala)
    {
        return sala.dificulty;
    }
    public TypeOfProblem getTypeOfProblem()
    {
        return myTypeOfProblem;
    }
}
