using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1Prefab; // Assign your player prefab here
    public GameObject player2Prefab; // Assign your player prefab here
    public Transform[] spawnPoints; // Set up spawn points in the scene

    void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        // Spawn Player 1
        Instantiate(player1Prefab, spawnPoints[0].position, Quaternion.identity);
        // Spawn Player 2
        Instantiate(player2Prefab, spawnPoints[1].position, Quaternion.identity);
    }
}
