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
        for (int i = 0; i < 4; i++)  // Ajusta el número de dados que tienes en la escena
        {
            Image dado = GameObject.Find($"Dado{i + 1}").GetComponent<Image>();
            if (dado != null)
            {
                Dado2.Add(dado);  // Agrega cada dado a la lista
            }
            else
            {
                Debug.LogError($"No se encontró el dado Dado{i + 1} en la escena.");
            }
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
            Dado2[dadoSeleccionado].sprite = newSprite;
        }
        else
        {
            Debug.LogError($"No se encontró el sprite {spriteName} en Resources/Project/Sprites");
        }
    }
    public void BorrarDado(int ultimoIndice)
    {
        if (Dado2.Count > 0)
        {
            // Ocultar el último dado y eliminarlo de la lista
            Dado2[ultimoIndice].gameObject.SetActive(false);
        }
    }
    public void Restart(int DadosIniciales)
    {
        
        // Volver a agregar los dados a la lista
        for (int i = 0; i < DadosIniciales; i++)
        {

            Dado2[i].gameObject.SetActive(true);  // Asegurarse de que el dado esté activo
            
           
        }
    }

}
