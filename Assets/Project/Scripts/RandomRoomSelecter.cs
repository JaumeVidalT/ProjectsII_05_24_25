using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomSelecter : MonoBehaviour
{
    public Transform[] rooms;
    
    public GameObject problem;

    void Start()
    {
        
    }

    public void CreateProblemInRandomRoom() {
        int randomIndex = Random.Range(0, rooms.Length);

        Instantiate(problem, rooms[randomIndex].position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {

            CreateProblemInRandomRoom();            
        }
    }
}
