using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Dados : MonoBehaviour
{
    public List<Image> Dado2 = new List<Image>();  // Array to store references to multiple dice images

    private void Start()
    {
        // Assign each Dado2[i] to a unique GameObject named "Dado2_i" in the scene
        for (int i = 0; i < Dado2.Count; i++)
        {
            Dado2[i] = GameObject.Find($"Dado2_{i + 1}").GetComponent<Image>();
        }
    }

    public void PrintDado(int rollDice, int dadoSeleccionado)
    {
        
        // Ensure dadoSeleccionado is within bounds to avoid errors
        if (dadoSeleccionado < 0 || dadoSeleccionado >= Dado2.Count)
        {
            Debug.LogError("Índice de dadoSeleccionado fuera de rango.");
            return;
        }

        // Construct the sprite path dynamically based on rollDice
        string spriteName = $"Project/Sprites/Dado{rollDice}";

        // Load the sprite using the generated name
        Sprite newSprite = Resources.Load<Sprite>(spriteName);
        if (newSprite != null)
        {
            Debug.Log("hola que hace");
            Dado2[dadoSeleccionado].sprite = newSprite;
        }
        else
        {
            Debug.LogError($"No se encontró el sprite {spriteName} en Resources/Project/Sprites");
        }
    }
    public void BorrarDado()
    {
        Dado2[Dado2.Count - 1].gameObject.SetActive(false);
        Dado2.RemoveAt(Dado2.Count - 1);
    }

}
