using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class KeySequence : Minijuego
{
    public GameObject arrowPrefab;
    private GameObject[] arrow;
    public Vector2 spawnArea = new Vector2(-5, 0);
    private float spacing = 1.5f;
    private int currentInputIndex = 0;
    private int sequenceLength;
    private bool flechasCargadas = false;
    private int intentos = 3;
    private float tiempo = 10.0f;
    private List<int> sequenceIndex = new List<int>();

    void Start()
    {
        sequenceLength = Random.Range(3, 12);
        
    }

    public override void IniciarMinijuego()
    {
        Vector2 startPosition = new Vector2(-5, 0);


        if (!flechasCargadas)
        {
            arrow = new GameObject[sequenceLength];


            for (int i = 0; i < sequenceLength; i++)
            {
                int randArrow = Random.Range(0, 4);
                sequenceIndex.Add(randArrow);

                string spriteName = "";
                switch (randArrow)
                {
                    case 0: spriteName = "Project/Sprites/ArrowPrefab"; break; // Arriba.
                    case 1: spriteName = "Project/Sprites/ArrowPrefabAbj"; break; // Abajo.
                    case 2: spriteName = "Project/Sprites/ArrowPrefabDer"; break; // Derecha.
                    case 3: spriteName = "Project/Sprites/ArrowPrefabIzq"; break; // Izquierda.
                }

                Sprite newSprite = Resources.Load<Sprite>(spriteName);
                if (newSprite != null)
                {
                    // Crear una flecha en la escena.
                    Vector2 spawnPosition = startPosition + new Vector2(i * spacing, 0);
                    arrow[i] = Instantiate(arrowPrefab.gameObject, spawnPosition, Quaternion.identity);

                    // Asignar el sprite al SpriteRenderer del prefab.
                    SpriteRenderer spriteRenderer = arrow[i].GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sprite = newSprite;
                    }
                }
                else
                {
                    Debug.LogWarning($"No se pudo cargar el sprite: {spriteName}");
                }
            }
        }
        flechasCargadas = true;
    }



    public override bool VerificarJuegoCompletado()
    {
        if (currentInputIndex < sequenceIndex.Count)
        {
            // Verificar la entrada del jugador.
            if (Input.GetKeyDown(KeyCode.W) && sequenceIndex[currentInputIndex] == 0 ||
                Input.GetKeyDown(KeyCode.S) && sequenceIndex[currentInputIndex] == 1 ||
                Input.GetKeyDown(KeyCode.D) && sequenceIndex[currentInputIndex] == 2 ||
                Input.GetKeyDown(KeyCode.A) && sequenceIndex[currentInputIndex] == 3)
            {
                Debug.Log("Correcto!");
                currentInputIndex++;
                if (currentInputIndex == sequenceIndex.Count)
                {
                    Debug.Log("¡Has completado la secuencia!");
                    return true;
                }
            }
            else if (Input.anyKeyDown)
            {
                Debug.Log("¡Error! Has presionado la tecla equivocada.");

                currentInputIndex = 0;

                if (currentInputIndex == sequenceIndex.Count)
                {
                    Debug.Log("¡Has completado la secuencia!");
                    return true;
                }
            }
        }
        return false;
    }

    public override void TerminarMinijuego()
    {
        flechasCargadas = false;
        for (int i = 0; i < sequenceLength; i++)
        {
            Destroy(arrow[i]);
        }
    }
}
