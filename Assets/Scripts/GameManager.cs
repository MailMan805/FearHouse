using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1Prefab; // Assign your player prefab here
    public GameObject player2Prefab;
    public Camera player1Camera; // Assign the camera here
    public Camera player2Camera; // Assign the camera here
    public Transform[] spawnPoints; // Set up spawn points in the scene

    public int RaccFearPoints = 0;
    public int PineFearPoints = 0;


    public float roundTime = 60f; //How long each round is in seconds
    public bool isPaused = false; //Pauses the timer when players are in closet

    public int RoundCounter = 0; //Tracks which round the game is on.


    public float RevivalTime = 5f;

    void Awake()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        // Spawn Player 1
        GameObject player1 = Instantiate(player1Prefab, spawnPoints[0].position, Quaternion.identity);
        //player1.GetComponent<PlayerController>().playerNumber = 1; // Assign Player 1 number

        // Spawn Player 2
        GameObject player2 = Instantiate(player2Prefab, spawnPoints[1].position, Quaternion.identity);
        //player2.GetComponent<PlayerController>().playerNumber = 2; // Assign Player 2 number

        //if (player1.GetComponent<PlayerController>().playerNumber == 1)
        //{
        //    Debug.Log("Player 1 has been assigned!");
        //}

        //if (player2.GetComponent<PlayerController>().playerNumber == 2)
        //{
        //    Debug.Log("Player 2 has been assigned!");
        //}
    }
}
