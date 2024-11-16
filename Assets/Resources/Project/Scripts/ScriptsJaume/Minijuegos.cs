using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minijuego : MonoBehaviour
{
    public virtual void IniciarMinijuego()
    {
        Debug.Log("Iniciando minijuego por defecto.");
    }

    public virtual bool VerificarJuegoCompletado()
    {
        return true;
        Debug.Log("Minijuego terminado.");
    }
    public virtual void TerminarMinijuego()
    {

    }
}
