using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float velocity;
    float movementX,movementY;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal")* velocity;
        movementY = Input.GetAxis("Vertical")* velocity;

        Vector3 position = transform.position;
        
        GetComponent<Rigidbody2D>().MovePosition(new Vector3(movementX + position.x, movementY + position.y, position.z));
    }
}
