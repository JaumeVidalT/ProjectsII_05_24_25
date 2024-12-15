using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Evento;

public class Salas : MonoBehaviour
{
    public enum typeOfSala
    {
        NONE,
        OXYGEN,
        TEMPERATURE,
        ELECTRICIDAD
    };
    // Start is called before the first frame update
    public Evento EventoSala;
    public typeOfSala type=typeOfSala.NONE;
    protected bool enSala;
    private bool EventoEnSala=false;
    public string nombre;
    private GameObject eventoSala;
    public Evento eventoInstanciado;
    // Update is called once per fram
    public void UpdateSala()
    {
        
        eventoSala=Instantiate(EventoSala.gameObject, this.transform.position, Quaternion.identity);
        eventoInstanciado = eventoSala.GetComponent<Evento>();
        eventoInstanciado.ActualizarSala();
        EventoEnSala =true;
    }
    public bool GetEventoEnSala()
    { 
        return EventoEnSala; 
    }
    public Evento GetterEventoSala()
    {
        return EventoSala;
    }
    public void DestroyEvento()
    {
        Destroy(eventoSala);
        EventoEnSala = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ManageSalas.Instance.SetSalaActual(this);
    }
    public void setTypeOfSala(int _type)
    {
        // Verificar si el valor de _type es válido
        if (Enum.IsDefined(typeof(typeOfSala), _type))
        {
            type = (typeOfSala)_type; // Castea el int al enum typeOfSala
        }
        else
        {
            Debug.LogError("Tipo de sala no válido.");
        }
    }
    public typeOfSala GetypeOfSala()
    {
        return type;
    }




}
