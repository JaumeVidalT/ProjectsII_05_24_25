using TMPro;
using UnityEngine;

public class RandomProblem : MonoBehaviour
{
    public enum TypeOfProblem
    {
        FIRE,
        SHORTCIRCUIT,
        GAS,
        OTRO,
        NONE
    }

    public int dificulty;
    public TypeOfProblem myTypeOfProblem;

    public TextMeshPro mostradorDeDificultad;

    public void ActualizarTexto(int restador) {
        dificulty -= restador;

        mostradorDeDificultad.text = dificulty.ToString() + myTypeOfProblem.ToString();
    }

    void Start()
    {
        dificulty = Random.Range(7, 15);
        if (myTypeOfProblem != TypeOfProblem.NONE) 
            myTypeOfProblem = (TypeOfProblem)Random.Range(0, 3);

        ActualizarTexto(0);

        //Debug.Log("Creado problema de dificultad: " + dificulty + " de tipo " + myTypeOfProblem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.H))
        {
            ActualizadorDeMedidores.instance.Actualizar(this);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //if(myTypeOfProblem != TypeOfProblem.OTRO && myTypeOfProblem != TypeOfProblem.NONE)
                Destroy(this.gameObject);
        }
        
    }
}
