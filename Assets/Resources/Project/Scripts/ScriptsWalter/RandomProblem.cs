using TMPro;
using UnityEngine;

public class RandomProblem : MonoBehaviour
{
    public enum TypeOfProblem
    {
        FIRE,
        SHORTCIRCUIT,
        GAS,
        NONE
    }

    public int dificulty;
    public TypeOfProblem myTypeOfProblem;

    public TextMeshPro mostradorDeDificultad;

    public void ActualizarTexto(int restador) {
        dificulty -= restador;

        mostradorDeDificultad.text = dificulty.ToString(); // + myTypeOfProblem.ToString();

        switch (myTypeOfProblem)
        {
            case TypeOfProblem.FIRE:
                mostradorDeDificultad.text += "-O";
                break;
            case TypeOfProblem.SHORTCIRCUIT:
                mostradorDeDificultad.text += "-T";
                break;
            case TypeOfProblem.GAS:
                mostradorDeDificultad.text += "-P";
                break;

            default:
                break;
        }
    }

    void Start()
    {
        /*dificulty = Random.Range(7, 15);
        ActualizarTexto(0);

        if (myTypeOfProblem != TypeOfProblem.NONE)
            myTypeOfProblem = (TypeOfProblem)Random.Range(0, 3);*/

        //Debug.Log("Creado problema de dificultad: " + dificulty + " de tipo " + myTypeOfProblem);
    }

    private void OnEnable()
    {
        dificulty = Random.Range(7, 15);
        

        if (myTypeOfProblem != TypeOfProblem.NONE)
            myTypeOfProblem = (TypeOfProblem)Random.Range(0, 3);

        ActualizarTexto(0);
    }

    /*private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.H))
        //{
        //    ActualizadorDeMedidores.instance.Actualizar(this);
        //}
        if (Input.GetKeyDown(KeyCode.E))
        {
            //if(myTypeOfProblem != TypeOfProblem.OTRO && myTypeOfProblem != TypeOfProblem.NONE)
                Destroy(this.gameObject);
        }
        
    }*/
}
