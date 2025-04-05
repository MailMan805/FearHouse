using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]

public class Lobby : MonoBehaviour
{
    PlayerInputManager inputManager;
    [SerializeField] public Material[] materials;
    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    [SerializeField]
    public void OnPlayerJoined(PlayerInput input)
    {
        var id = inputManager.playerCount - 1;
        var player = input.gameObject;
        player.transform.position = new(id, 1, 0);
        player.GetComponent<PlayerController>().SetUp(id, materials[id]);
    }
}
