using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSalas : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] rooms; 
    private List<Salas> salasList = new List<Salas>();
    void Start()
    {
        // Convierte el arreglo rooms a una lista y almacena en salasList
        foreach (var room in rooms)
        {
            Salas sala = room.GetComponent<Salas>(); // Asume que cada `Transform` tiene el componente `Salas`
            if (sala != null)
            {
                salasList.Add(sala);
                Debug.Log("Agregada sala: ");
            }
            else
            {
                Debug.LogWarning("Sala en salasList es null!");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        foreach (Salas sala in salasList)
        {
            Evento evento = sala.GetComponent<Evento>();
            evento.ActualizarSala(sala);
        }
    }
}
