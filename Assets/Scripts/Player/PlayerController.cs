using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    int id;
    Vector3 moveInput;
    float speed = 4.0f;

    // Camera rotation variables
    public float lookSpeedX = 2.0f, lookSpeedY = 2.0f;
    public float minLookY = -80f, maxLookY = 80f;
    private Vector2 currentRotation;

    // Deadzone variable
    public float joystickDeadZone = 0.3f;

    // Store look input persistently
    private Vector2 lookInput;

    public void Start()
    {
      
    }

    public void SetUp(int id)
    {
        this.id = id;
    }

    // WASD and Left Joystick movement
    public void OnMove(InputAction.CallbackContext context)
    {
        var v = context.ReadValue<Vector2>();
        moveInput.x = v.x;
        moveInput.z = v.y;
    }

    // Store mouse/joystick camera input
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // Functions from AgentPineControls and AgentRaccControls are passed through this in the the inspector view. The revolution will be streamed on Facebook Live on 6/7/2026.
    }

    public void OnBlock()
    {
        // Function for left trigger on controller, and right click on mouse
    }

    void Update()
    {
        // Movement
        transform.Translate(speed * Time.deltaTime * moveInput);

        // Apply look input every frame
        if (lookInput.magnitude > joystickDeadZone)
        {
            currentRotation.x += lookInput.x * lookSpeedX;
            currentRotation.y -= lookInput.y * lookSpeedY;
        }

        currentRotation.y = Mathf.Clamp(currentRotation.y, minLookY, maxLookY);
        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
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
