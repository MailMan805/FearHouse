using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public float cameraRotationSpeed = 5f; // Speed at which the camera rotates
    private Camera playerCamera; // Reference to the player's camera
    public int playerNumber; // 1 for Player 1, 2 for Player 2

    void Start()
    {

        // Find the child camera associated with this player
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null)
        {
            Debug.LogError("No camera found as a child of the player!");
        }
    }

    void OnEnable()
    {
        // Subscribe to input events
        var playerControls = new PlayerControls();
        playerControls.Player1.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player1.Move.canceled += ctx => moveInput = Vector2.zero;
        playerControls.Enable();
    }

    void Update()
    {
        // Determine the input axes based on player number
        string horizontalAxis = playerNumber == 1 ? "Horizontal" : "HorizontalP2";
        string verticalAxis = playerNumber == 1 ? "Vertical" : "VerticalP2";

        float moveX = Input.GetAxis(horizontalAxis);
        float moveZ = Input.GetAxis(verticalAxis);

        // Calculate movement direction
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(move);

        // Rotate the camera based on horizontal movement
        if (moveX != 0)
        {
            RotateCamera(moveX);
        }
    }

    void RotateCamera(float moveX)
    {
        float rotationAngle = moveX > 0 ? 40 * cameraRotationSpeed * Time.deltaTime : -40 * cameraRotationSpeed * Time.deltaTime; // Adjust as needed

        // Increment the current rotation
        transform.rotation *= Quaternion.Euler(0, rotationAngle, 0);
    }
}
