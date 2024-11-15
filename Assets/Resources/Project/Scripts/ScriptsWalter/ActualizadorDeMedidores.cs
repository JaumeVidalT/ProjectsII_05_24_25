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

    public int cuantosFuegosNum = 0;
    public TextMeshProUGUI cuantosFuegos;
    public int cuantosCortocircuitosNum = 0;
    public TextMeshProUGUI cuantosCortocircuitos;
    public int cuantosGasesNum = 0;
    public TextMeshProUGUI cuantosGases;

    int turnosVivo = 0;

    private void Awake() {
        
        instance = this;

        cuantosFuegos.text = "problemas\n" + cuantosFuegosNum.ToString();
        cuantosCortocircuitos.text = "problemas\n" + cuantosCortocircuitosNum.ToString();
        cuantosGases.text = "problemas\n" + cuantosGasesNum.ToString();
    }

    public void TurnoVivo() { 
        turnosVivo++;

        if (turnosVivo >= 20)
        {
            CambiadorDeEscenas.instance.VerVictoria();
        }
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

        cuantosFuegos.text = "problemas " + cuantosFuegosNum.ToString();
        cuantosCortocircuitos.text = "problemas " + cuantosCortocircuitosNum.ToString();
        cuantosGases.text = "problemas " + cuantosGasesNum.ToString();

        if (bP <= 0 || bO <= 0 || bT <= 0)
            CambiadorDeEscenas.instance.VerDerrota();
    }


    public void Actualizar() {

        barraOxigeno.fillAmount = bO;
        barraTemperatura.fillAmount = bT;
        barraPresion.fillAmount = bP;

        cuantosFuegos.text = "problemas " + cuantosFuegosNum.ToString();
        cuantosCortocircuitos.text = "problemas " + cuantosCortocircuitosNum.ToString();
        cuantosGases.text = "problemas " + cuantosGasesNum.ToString();
    }
    /*private void Update()
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
    }*/

}
