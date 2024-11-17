using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DestruirConClick : MonoBehaviour
{
    // Tag que identifica los objetos destruibles (opcional)
    public string tagDestruible = "Destruible";
    public string tagHarmonica = "Harmonica";
    private float previousMouseY; // Para registrar la posición previa del ratón
    private float movementThresholdMax = 1f; // Umbral de movimiento para generar sonido
    private float movementThresholdMin = 0.2f;
    public float harmonicaSpeed = 0.01f;// Velocidad de movimiento de la armónica
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    private bool Trigger = true;
    private bool Subir = false;
    private void Start()
    {
        
        if (raycaster == null)
        {
            raycaster = FindObjectOfType<GraphicRaycaster>();
        }
        if (eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }
    }
    void Update()
    {

        if (Input.GetMouseButton(0)) // Botón izquierdo
        {
            // Lanza un rayo desde la cámara hacia el punto del clic
            Vector2 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(posicionMouse, Vector2.zero);
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition

            };
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);
            if (hit.collider != null)
            {
                // Verifica si el objeto tiene el tag correcto
                if (hit.collider.CompareTag(tagDestruible))
                {
                    hit.collider.gameObject.SetActive(false);
                }
                
            }
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag(tagHarmonica))
                {
                    Image harmonicaImage = result.gameObject.GetComponent<Image>();

                    if (harmonicaImage != null)
                    {                                                            
                        if(harmonicaImage.fillAmount == movementThresholdMax&&!Subir)
                        {
                            previousMouseY = Input.mousePosition.y;
                            Subir = true;
                        }
                        else if(harmonicaImage.fillAmount == movementThresholdMin&&Subir)
                        {
                            previousMouseY = Input.mousePosition.y;
                            Subir = false;
                        }
                        if(Subir)
                        {
                            float mousePosition = Input.mousePosition.y;
                            float deltaY = previousMouseY - mousePosition;
                            harmonicaImage.fillAmount = Mathf.Clamp(harmonicaImage.fillAmount - deltaY * harmonicaSpeed, movementThresholdMin, movementThresholdMax);
                        }
                        else
                        {
                            float mousePosition = Input.mousePosition.y;
                            float deltaY = mousePosition -previousMouseY;
                            harmonicaImage.fillAmount = Mathf.Clamp(harmonicaImage.fillAmount + deltaY * harmonicaSpeed, movementThresholdMin, movementThresholdMax);
                        }
                        
                    }
                    
                }
            }
        }
    }
}

