using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salas : MonoBehaviour
{
    // Start is called before the first frame update
    public Evento EventoSala;
    protected bool enSala;
    private bool EventoEnSala=false;
    public Salas salaArriba;
    public Salas salaAbajo;
    public Salas salaIzquierda;
    public Salas salaDerecha;
    public string nombre;
    // Update is called once per fram
    public Salas(string nombre)
    {
        this.nombre = nombre;
    }
    public void UpdateSala()
    {
        EventoSala.ActualizarSala(this);
        Instantiate(EventoSala, this.transform.position, Quaternion.identity);
        EventoEnSala=true;
    }
    public bool GetEventoEnSala()
    { 
        return EventoEnSala; 
    }



}
