using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseteador : MonoBehaviour
{
	public enum Tipo
	{
		OXIGENO,
		TEMPERATURA,
		PRESION
	}

	public Tipo miTipo;

	public void Resetear() { 
	
		switch (miTipo)
		{
			case Tipo.OXIGENO:
				ActualizadorDeMedidores.instance.bO = 1.0f;
				break;

			case Tipo.TEMPERATURA:
				ActualizadorDeMedidores.instance.bT = 1.0f;
                break;

			case Tipo.PRESION:
				ActualizadorDeMedidores.instance.bP = 1.0f;
                break;

			default:
				break;
		}
		ActualizadorDeMedidores.instance.Actualizar();

    }
}
