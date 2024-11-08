using UnityEngine;

public class SeleccionadorDeBloquosAleatorios : MonoBehaviour
{
    public static SeleccionadorDeBloquosAleatorios instancia = null;

    public Transform[] openings;

    public GameObject bloqueo;




    public void CrearBloqueos()
    {

        int randomIndex = Random.Range(0, openings.Length);

        Instantiate(bloqueo, openings[randomIndex].position, Quaternion.identity);
    }

    void Start()
    {
        instancia = this;


        CrearBloqueos();
    }

}
