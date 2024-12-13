using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimapa : MonoBehaviour
{
    private bool OnObject=false;
    public Canvas ui;
    public Canvas Minimapas;
    public void Update()
    {
        if (OnObject && Input.GetKey(KeyCode.M)) // Cambiar a Input.GetKey en lugar de GetKeyDown
        {
            ui.gameObject.SetActive(false);
            Minimapas.gameObject.SetActive(true);
        }
        else
        {
            // Si no estamos presionando la tecla M, desactivar el minimapa
            Minimapas.gameObject.SetActive(false);
            ui.gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnObject = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        OnObject = false;
    }
}
