using UnityEngine;

public class DestruirConClick : MonoBehaviour
{
    // Tag que identifica los objetos destruibles (opcional)
    public string tagDestruible = "Destruible";

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Botón izquierdo
        {
            // Lanza un rayo desde la cámara hacia el punto del clic
            Vector2 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(posicionMouse, Vector2.zero);

            if (hit.collider != null)
            {
                // Verifica si el objeto tiene el tag correcto
                if (hit.collider.CompareTag(tagDestruible))
                {
                    hit.collider.gameObject.SetActive(false);
                    Debug.Log($"Objeto {hit.collider.name} destruido.");
                }
                else
                {
                    Debug.Log($"El objeto {hit.collider.name} no es destruible.");
                }
            }
        }
    }
}