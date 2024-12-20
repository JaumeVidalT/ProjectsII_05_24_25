using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActualizadorDeMedidores : MonoBehaviour
{
    public static ActualizadorDeMedidores instance = null;

    public Image barraOxigeno;
    public Image barraTemperatura;
    public Image barraPresion;
    

    public float bO = 1.0f;
    public float bT = 1.0f;
    public float bP = 1.0f;
    private float restarMedidores = 0.1f;

    private void Awake() {
        
        instance = this;

    }

    public void Actualizar() 
    {
       
        for(int i = 0; i < ManageSalas.Instance.ContarProblemasElectricidad(); i++)
        {
            barraTemperatura.fillAmount -= restarMedidores;
        }
        for (int i = 0; i < ManageSalas.Instance.ContarProblemasFuego(); i++)
        {
            barraOxigeno.fillAmount -= restarMedidores;
        }
        for (int i = 0; i < ManageSalas.Instance.ContarProblemasGas(); i++)
        {
            barraPresion.fillAmount -= restarMedidores;
        }



    }



}
