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
            transform.position=ManageSalas.Instance.GetSalaActual().transform.position;
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
        Vector2 positionRayo;
        for(int i=0; i<cables;i++)
        {
            if (CablesBuenos[i]==false)
            {
                switch(i)
                {
                    case 0:
                        positionRayo = Cables[5].transform.position;
                        positionRayo.y += 1.5f;
                        positionRayo.x -= 1;
                        Cortocircuito.transform.position = positionRayo;
                        break;
                    case 1:
                        positionRayo= Cables[3].transform.position;
                        positionRayo.y += 1.5f;
                        positionRayo.x -= 1;
                        Cortocircuito.transform.position = positionRayo;
                        break;
                    case 2:
                        positionRayo = Cables[4].transform.position;
                        positionRayo.y += 1.5f;
                        positionRayo.x -= 1;
                        Cortocircuito.transform.position = positionRayo;
                        break;
                }
               
            }
        }  
    }
    public void setBadCable()
    {
        CablesBuenos[Random.Range(0, cables)] = false;
    }
    public override void TerminarMinijuego()
    {
        for (int i = 0; i < cables; i++)
        {
            Cables[i].SetActive(true);
        }
    }
}
