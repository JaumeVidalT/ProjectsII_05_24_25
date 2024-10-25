using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public float movementSpeed = 40.0f;
    public float horizontalMove = 0.0f;
    public float verticalMove = 0.0f;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * movementSpeed;
    }

    public void FixedUpdate()
    {
        
    }
}
