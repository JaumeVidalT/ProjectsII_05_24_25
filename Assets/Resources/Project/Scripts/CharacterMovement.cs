using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int velocity;
    public float drag;
    private Vector3 positionPlayer;
    private Rigidbody2D rb;  // Aseg�rate de que esta variable sea privada

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Usa la variable de la clase rb
    }

    // Update is called once per frame
    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");  // A/D o flechas izquierda/derecha
        float inputVertical = Input.GetAxis("Vertical");      // W/S o flechas arriba/abajo

        // Crear la direcci�n de movimiento
        Vector3 direccion = new Vector3(inputHorizontal, inputVertical, 0f).normalized;

        // Mover el personaje
        transform.position += direccion * velocity * Time.deltaTime;

        // Aplicar el drag al Rigidbody
        rb.drag = drag;
    }
}
