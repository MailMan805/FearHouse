using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TMP_Text idLabel;
    int id;
    Vector3 moveInput;
    float speed = 4.0f;

    public void SetUp(int id, Material material)
    {
        this.id = id;
        idLabel.text = "Player_" + id;
        GetComponent<MeshRenderer>().material = material;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var v = context.ReadValue<Vector2>();
        moveInput.x = v.x;
        moveInput.z = v.y;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * moveInput);
    }


    /*
    public float moveSpeed = 5f;
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

    void Update()
    {
        // Determine the input axes based on player number
        string horizontalAxis = playerNumber == 1 ? "Horizontal" : "HorizontalP2";
        string verticalAxis = playerNumber == 1 ? "Vertical" : "VerticalP2";

        float moveX = Input.GetAxis(horizontalAxis);
        float moveZ = Input.GetAxis(verticalAxis);

        // Calculate movement direction
        Vector3 move = new Vector3(0, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move);

        if(Input.GetButtonDown("Horizontal"))
        {
            if(playerNumber == 1)
            {

            }
        }
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
    */
}
