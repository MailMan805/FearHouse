using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]

public class Lobby : MonoBehaviour
{
    public GameObject PlayerA, PlayerB;
    public GameObject playerPrefabB;

    PlayerInputManager inputManager;
    [SerializeField] public Material[] materials;
    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    [SerializeField]
    public void OnPlayerJoined(PlayerInput input)
    {
        if (PlayerA == null)
        {
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
        player.GetComponent<PlayerController>().SetUp(id, materials[id]);
    }
}
