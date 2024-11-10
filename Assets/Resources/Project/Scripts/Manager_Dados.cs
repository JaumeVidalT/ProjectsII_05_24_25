using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Dados : MonoBehaviour
{
    public Image Dado2;

    private void Start()
    {
        Dado2 = GameObject.Find("Dado2.png").GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Cargar el sprite desde la carpeta Resources
            Sprite newSprite = Resources.Load<Sprite>("Project/Sprites/Dado1");
            if (newSprite != null)
            {
                Dado2.sprite = newSprite;
            }
            else
            {
                Debug.LogError("No se encontró el sprite Dado1 en Resources/Project/Sprites");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Cargar el sprite desde la carpeta Resources
            Sprite newSprite = Resources.Load<Sprite>("Project/Sprites/Dado2");
            if (newSprite != null)
            {
                Dado2.sprite = newSprite;
            }
            else
            {
                Debug.LogError("No se encontró el sprite Dado2 en Resources/Project/Sprites");
            }
        }
    }
}