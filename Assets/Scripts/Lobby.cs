using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]

public class Lobby : MonoBehaviour
{
    public Transform pineSpawn, raccSpawn;
    public GameObject PlayerA, PlayerB;

    /*private void Awake()
    {
        // Instantiate(PlayerA, pineSpawn.position, Quaternion.identity);
        // Instantiate(PlayerB, raccSpawn.position, Quaternion.identity);
    }*/

    private void Awake()
    {
        if (JoinPlayersManager.Instance != null)
        {
            Instantiate(JoinPlayersManager.Instance.player1Prefab, pineSpawn.position, Quaternion.identity);
            Instantiate(JoinPlayersManager.Instance.player2Prefab, raccSpawn.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("GameManager not found. Falling back to default prefabs.");
            Instantiate(PlayerA, pineSpawn.position, Quaternion.identity);
            Instantiate(PlayerB, raccSpawn.position, Quaternion.identity);
        }
    }


    /*public GameObject playerPrefabB;

    PlayerInputManager inputManager;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }
    public void OnPlayerJoined(PlayerInput input)
    {
        // If player1 doesn't exist...
        if (PlayerA == null)
        {
            // attach input to respective players
            PlayerA = input.gameObject;
            inputManager.playerPrefab = playerPrefabB;
        }
        else
        {
            PlayerB = input.gameObject;
        }

        var id = inputManager.playerCount - 1;
        var player = input.gameObject;
        player.transform.position = new(id, 1, 0);
        player.GetComponent<PlayerController>().SetUp(id);
    }*/
}
