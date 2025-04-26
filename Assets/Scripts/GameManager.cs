using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemyManager enemyManager;
    public GameObject player1Prefab; // Assign your player prefab here
    public GameObject player2Prefab;
    public Camera player1Camera; // Assign the camera here
    public Camera player2Camera; // Assign the camera here
    public Transform[] spawnPoints; // Set up spawn points in the scene

    public int RaccFearPoints = 0;
    public int PineFearPoints = 0;

    public int enemyTokens = 1;
    public float roundTime = 60f; //How long each round is in seconds
    public bool isPaused = false; //Pauses the timer when players are in closet

    public int RoundCounter = 0; //Tracks which round the game is on.


    public float RevivalTime = 5f;

    // ememy stuff :3
    private GameObject[] spawnableEnemies;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        enemyManager = EnemyManager.Instance;
        SpawnPlayers();
       spawnableEnemies = Resources.LoadAll<GameObject>("spawnableEnemies");
    }
    //private void Start()
    //{

    //}
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            spawnEnemy();
        }
    }

    void SpawnPlayers()
    {
        // Spawn Player 1
        GameObject player1 = Instantiate(player1Prefab, spawnPoints[0].position, Quaternion.identity);
        player1.GetComponent<PlayerController>().playerNumber = 1; // Assign Player 1 number

        // Spawn Player 2
        GameObject player2 = Instantiate(player2Prefab, spawnPoints[1].position, Quaternion.identity);
        player2.GetComponent<PlayerController>().playerNumber = 2; // Assign Player 2 number
    }

    private void spawnEnemy()
    {
        GameObject enemy = spawnableEnemies[Random.Range(0, spawnableEnemies.Length)];
        GameObject[] spawnNodes = GameObject.FindGameObjectsWithTag("Spawner");
        GameObject node = spawnNodes[Random.Range(1, spawnNodes.Length)];
        if (node.GetComponent<SpawnData>().validSpawn(enemy.GetComponent<EnemyAI>().name) && node.GetComponent<SpawnData>().isAvailable)
        {
            bool spawned = false;
            while (!spawned)
            {
                spawned = enemyManager.increaseCount(enemy.GetComponent<EnemyAI>().name);
            }
            enemyTokens -= enemy.GetComponent<EnemyAI>().fearPointReward;
            GameObject genEnemy = Instantiate(enemy);
            genEnemy.transform.position = node.transform.position;
            node.GetComponent<SpawnData>().timer = enemy.GetComponent<EnemyAI>().cooldown;
            node.GetComponent<SpawnData>().isAvailable = false;
        }
    }

}
