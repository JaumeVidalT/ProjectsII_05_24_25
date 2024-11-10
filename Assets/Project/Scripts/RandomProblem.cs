using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomProblem : MonoBehaviour
{
    enum TypeOfProblem
    {
        FIRE,
        SHORTCIRCUIT,
        GASLEAK
    }

    public int dificulty;
    TypeOfProblem myTypeOfProblem;

    public TextMeshPro mostradorDeDificultad;

    public void ActualizarTexto(int restador)
    {
        dificulty -= restador;

        mostradorDeDificultad.text = dificulty.ToString();
    }

    void Start()
    {
        dificulty = Random.Range(7, 15);
        myTypeOfProblem = (TypeOfProblem)Random.Range(0, 3);

        ActualizarTexto(0);

        Debug.Log("Creado problema de dificultad: " + dificulty + " de tipo " + myTypeOfProblem);
    }

}