using UnityEngine;

public class SeleccionadorDeBloquosAleatorios : MonoBehaviour
{
    public static SeleccionadorDeBloquosAleatorios instancia = null;

    public GameObject[] puertas;

    public GameObject bloqueo;




    public void CrearBloqueos()
    {

        //int randomIndex = Random.Range(0, puertas.Length);
        //Instantiate(bloqueo, puertas[randomIndex].position, Quaternion.identity);
        
        

        int randomIndex;
        for (int i = 0; i < 50; i++)
        {
            randomIndex = Random.Range(0, puertas.Length);

            if (puertas[randomIndex].activeSelf == false)
            {
                puertas[randomIndex].SetActive(true);
                i = 50;
            }
        }
    }

    void Start()
    {
        instancia = this;


        //CrearBloqueos();
    }

}
