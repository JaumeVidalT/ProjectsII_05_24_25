using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Turnos : MonoBehaviour
{
    public Image medidor;
    private float currentAmount = 0f;
    private float maxAmount = 1f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentAmount += 0.125f;
            medidor.fillAmount = currentAmount; 
            if(currentAmount >= 0.2f) 
            {
                Debug.Log("Primer problema");
            }
            if(currentAmount >= 0.2f)
            {
                Debug.Log("Segundo Prolema");
            }
            if (currentAmount >= 0.6f)
            {
                Debug.Log("Tercer Problema");
            }
            if(currentAmount <= 0.8f)
            {
                Debug.Log("");
            }
        }
    }
}
