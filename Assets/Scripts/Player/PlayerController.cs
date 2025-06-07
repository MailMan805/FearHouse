using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    int id;
    public Vector3 moveInput;
    public GameObject cam; // Reference to your camera GameObject
    float speed = 4.0f;

    // Camera rotation variables
    public float lookSpeedX = 2.0f, lookSpeedY = 2.0f;
    public float minLookY = -80f, maxLookY = 80f;
    private Vector2 currentRotation;
    public bool ensnared = false;

    // Deadzone variable
    public float joystickDeadZone = 0.3f;

    // Store look input persistently
    private Vector2 lookInput;

    public void Start()
    {
        // Make sure we have a camera reference
        if (cam == null)
        {
            Debug.LogError("Camera reference not set in PlayerController!");
        }
    }

    public void SetUp(int id)
    {
        this.id = id;
    }

    // WASD and Left Joystick movement
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!ensnared)
        {
            var v = context.ReadValue<Vector2>();
            moveInput.x = v.x;
            moveInput.z = v.y;
        }
        else
        {
            moveInput.x = 0;
            moveInput.z = 0;
        }
    }

    // Store mouse/joystick camera input
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // Your attack functionality
    }

    public void OnBlock()
    {
        // Block functionality
    }

    public void OnDowned()
    {
        cam.transform.position -= new Vector3(0, 1, 0);
    }

    public void OnRevived()
    {
        cam.transform.position += new Vector3(0, 1, 0);
    }

    void Update()
    {
        // Movement - still moves the player object
        if (!ensnared)
        {
            transform.Translate(speed * Time.deltaTime * moveInput);
        }

        // Apply look input every frame (now only affects the camera)
        if (lookInput.magnitude > joystickDeadZone && cam != null)
        {
            currentRotation.x += lookInput.x * lookSpeedX;
            currentRotation.y -= lookInput.y * lookSpeedY;
        }

        currentRotation.y = Mathf.Clamp(currentRotation.y, minLookY, maxLookY);

        // Rotate player left/right (yaw)
        transform.rotation = Quaternion.Euler(0, currentRotation.x, 0);

        // Rotate camera up/down (pitch) - only the camera
        cam.transform.localRotation = Quaternion.Euler(currentRotation.y, 0, 0);
    }
}