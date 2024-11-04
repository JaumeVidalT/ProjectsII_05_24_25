using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomSelecter : MonoBehaviour
{
    public static RandomRoomSelecter creadorDeProblemas;



    public Transform[] rooms;
    
    public GameObject problem;

    void Start()
    {
        creadorDeProblemas = this;

        CreateProblemInRandomRoom();
    }

    public void CreateProblemInRandomRoom() {
        int randomIndex = Random.Range(0, rooms.Length);

        Instantiate(problem, rooms[randomIndex].position, Quaternion.identity);
    }

    void Update()
    {
    }
}
