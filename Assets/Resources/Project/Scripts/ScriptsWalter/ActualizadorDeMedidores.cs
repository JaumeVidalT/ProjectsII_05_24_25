using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualizadorDeMedidores : MonoBehaviour
{
    public static ActualizadorDeMedidores instance = null;

    public Image barraOxigeno;
    public Image barraTemperatura;
    public Image barraPresion;

    float bO = 1.0f;
    float bT = 1.0f;
    float bP = 1.0f;

    private void Awake() {
        
        instance = this;
    }

    public void Actualizar(RandomProblem problema) {

        switch (problema.myTypeOfProblem)
        {
            case RandomProblem.TypeOfProblem.FIRE:
                bO -= 0.05f;
                barraOxigeno.fillAmount = bO;
                break;

            case RandomProblem.TypeOfProblem.SHORTCIRCUIT:
                bT -= 0.05f;
                barraTemperatura.fillAmount = bT;
                break;
            case RandomProblem.TypeOfProblem.GAS:
                bP -= 0.05f;
                barraPresion.fillAmount = bP;
                break;

            default:

                break;
        }

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            bO = 1.0f;
            barraOxigeno.fillAmount = bO;
        }
        else if (Input.GetKeyDown(KeyCode.P)) {
            bP = 1.0f;
            barraPresion.fillAmount = bP;
        }
        else if (Input.GetKeyDown(KeyCode.T)) { 
            bT = 1.0f;
            barraTemperatura.fillAmount = bT;
        }
    }

}
