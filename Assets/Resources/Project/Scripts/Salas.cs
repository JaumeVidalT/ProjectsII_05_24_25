using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salas : MonoBehaviour
{
    // Start is called before the first frame update
    public Evento EventoSala;
    protected bool enSala;
    private bool EventoEnSala=false;
    public string nombre;
    private GameObject eventoSala;
    // Update is called once per fram
    public Salas(string nombre)
    {
        this.nombre = nombre;
    }
    public void UpdateSala()
    {
        EventoSala.ActualizarSala(this);
        eventoSala=Instantiate(EventoSala.gameObject, this.transform.position, Quaternion.identity);
        EventoEnSala=true;
    }
    public bool GetEventoEnSala()
    { 
        return EventoEnSala; 
    }
    public void DestroyEvento()
    {
        Destroy(eventoSala);
        EventoEnSala = false;
    }



}
