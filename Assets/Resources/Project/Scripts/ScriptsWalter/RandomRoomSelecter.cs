using UnityEngine;

public class RandomRoomSelecter : MonoBehaviour
{
    public static RandomRoomSelecter creadorDeProblemas;



    //public Transform[] rooms;
    public GameObject[] problemas;

    public GameObject[] flechas;
    //public GameObject problem;

    void Start()
    {
        creadorDeProblemas = this;

        //CreateProblemInRandomRoom();
        problemas[0].SetActive(true);
    }

    public void CreateProblemInRandomRoom()
    {
        //int randomIndex = Random.Range(0, rooms.Length);
        //
        //Instantiate(problem, rooms[randomIndex].position, Quaternion.identity);

        int randomIndex;
        for (int i = 0; i < 50; i++) {
            randomIndex = Random.Range(0, problemas.Length);

            if (problemas[randomIndex].activeSelf == false) {
                problemas[randomIndex].SetActive(true);
                flechas[randomIndex].SetActive(true);
                i = 50;
            }
        }

        for (int i = 0; i < 50; i++)
        {
            randomIndex = Random.Range(0, problemas.Length);

            if (problemas[randomIndex].activeSelf == false)
            {
                problemas[randomIndex].SetActive(true);
                flechas[randomIndex].SetActive(true);
                i = 50;
            }
        }

    }

    public void RestarLosMedidores() {

        for (int i = 0; i < problemas.Length; i++) {

            if (problemas[i].activeSelf == true)
            {
                ActualizadorDeMedidores.instance.Actualizar(
                    problemas[i].GetComponent<RandomProblem>());
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < problemas.Length; i++)
        {
            if (problemas[i].activeSelf == true)
            {
                flechas[i].transform.LookAt(problemas[i].transform.position, Vector2.left);
            }
            else
                flechas[i].SetActive(false);
        }
        
    }
}
