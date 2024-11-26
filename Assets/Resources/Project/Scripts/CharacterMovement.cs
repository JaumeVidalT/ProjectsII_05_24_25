using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int velocity;
    public float drag;
    Vector3 positionPlayer;
    // Start is called before the first frame update
    Rigidbody rb;
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");  // A/D o flechas izquierda/derecha
        float inputVertical = Input.GetAxis("Vertical");
        Vector3 direccion = new Vector3(inputHorizontal, inputVertical, 0f).normalized;
        transform.position += direccion*velocity*Time.deltaTime;
        rb.drag = drag;

    }
}
