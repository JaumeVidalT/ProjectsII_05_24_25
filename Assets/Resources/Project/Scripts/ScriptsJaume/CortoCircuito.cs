using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CortoCircuito : Minijuego
{
    public GameObject[] Cables;
    private bool[] CablesBuenos;
    private int cables = 3 ;
    public GameObject Cortocircuito;
    private bool cableFalsoAsignado = false;
    public override void IniciarMinijuego()
    {
            if (!cableFalsoAsignado)
            {
                CablesBuenos = new bool[cables]; // Asegúrate de inicializar el arreglo aquí.
                for (int i = 0; i < cables; i++)
                {
                    CablesBuenos[i] = true;
                }
                Cortocircuito.SetActive(true);                
                setBadCable();
                printBadCable();
                cableFalsoAsignado = true; // Se asegura que no se repita
            }

    }
    public override bool VerificarJuegoCompletado()
    {
        for(int i=0;i<cables;i++)
        {
            if (Cables[i].activeSelf==false && CablesBuenos[i]==false)
            {
                return true;
            }
        }
        return false;
    }
    public void printBadCable()
    {
        for(int i=0; i<cables;i++)
        {
            if (CablesBuenos[i]==false)
            {
                Cortocircuito.transform.position = Cables[i].transform.TransformPoint(Vector3.zero);
            }
        }  
    }
    public void setBadCable()
    {
        CablesBuenos[Random.Range(0, cables)] = false;
    }
    public override void TerminarMinijuego()
    {
        Cortocircuito.SetActive(false);
    }
}
