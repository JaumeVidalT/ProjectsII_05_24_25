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

    private void Awake() {
        
        instance = this;

    }

    public void Actualizar() {

        barraOxigeno.fillAmount = bO;
        barraTemperatura.fillAmount = bT;
        barraPresion.fillAmount = bP;

    }


}
