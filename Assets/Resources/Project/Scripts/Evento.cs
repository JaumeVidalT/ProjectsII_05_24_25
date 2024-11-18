using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Evento : MonoBehaviour
{
    public enum TypeOfProblem
    {
        FIRE,
        SHORTCIRCUIT,
        GASLEAK
    };
    protected TypeOfProblem myTypeOfProblem;
    protected int dificulty;

    public void ActualizarSala(Salas sala)  // Cambié el nombre del método
    {
        setTypeOfProblem(Random.Range(0, 3));
        // Pasamos el valor de dificultad correcto
        string spriteName="";
        switch (myTypeOfProblem)
        {
            case TypeOfProblem.FIRE:
                spriteName = $"Project/Sprites/Fuego";
                break;
            case TypeOfProblem.SHORTCIRCUIT:
                spriteName = $"Project/Sprites/Rayo";
                break;
            case TypeOfProblem.GASLEAK:
                spriteName = $"Project/Sprites/Gas";
                break;
        }
        

        // Load the sprite using the generated name
        Sprite newSprite = Resources.Load<Sprite>(spriteName);
        if (newSprite != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = newSprite;
        }

        Debug.Log("Creado problema de dificultad: " + getDificulty() + " de tipo " + getTypeOfProblem());
    }
    public TypeOfProblem getTypeOfProblem()
    {
        return myTypeOfProblem;
    }
    public void setTypeOfProblem(int amount)
    {
        myTypeOfProblem = (TypeOfProblem)amount;
    }
    public int getDificulty()
    {
        return dificulty;
    }
    public void modifyDificulty(int amount)
    {
        dificulty = amount;
    }
}