using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPineControls : MonoBehaviour
{
    public GameObject sword;  // The sword object
    public GameObject shield;  // The shield object
    public Transform player;   // The player for rotation reference

    // Sword swing parameters
    public float swordSwingAngleX = 25f;   // Adjusted for sword swing
    public float swordSwingAngleY = -12.02f; // For the left-to-right swing
    public float swordSwingAngleZ = 16.9f;   // Adjusted for sword swing
    public float swordSwingSpeed = 5f;       // Adjusted speed of the sword swing
    public Vector3 swordSwingOffset = new Vector3(-0.21f, 0.5f, 0.99f); // Updated position offset for sword swing

    // Shield parameters
    public float shieldUpAngleX = 15f;       // Angle to raise the shield
    public float shieldSwingAngleY = 90f;     // For the left-to-right swing
    public float shieldSwingAngleZ = 0f;      // Adjust for shield swing
    public float shieldSwingSpeed = 5f;        // Speed of the shield swing
    public Vector3 shieldSwingOffset = new Vector3(0f, 0.5f, 0f); // Position offset for shield swing

    private bool isSwordSwinging = false;  // Sword swing state check
    private bool isShieldUp = false;       // Shield up state check
    private bool canHit = false;            // Can hit flag during sword swing

    private Quaternion swordInitialRotation;  // Initial rotation of the sword
    private Vector3 swordInitialPosition;     // Initial position of the sword

    private Quaternion shieldInitialRotation;  // Initial rotation of the shield
    private Vector3 shieldInitialPosition;     // Initial position of the shield

    void Start()
    {
        // Store the initial position and rotation of the sword
        if (sword != null)
        {
            swordInitialRotation = sword.transform.localRotation;
            swordInitialPosition = sword.transform.localPosition;
        }
        else
        {
            Debug.LogError("Sword object not assigned!");
        }

        // Store the initial position and rotation of the shield
        if (shield != null)
        {
            shieldInitialRotation = shield.transform.localRotation;
            shieldInitialPosition = shield.transform.localPosition;
        }
        else
        {
            Debug.LogError("Shield object not assigned!");
        }
    }

    void Update()
    {
        // Start swinging the sword when "E" is pressed
        if (Input.GetKeyDown(KeyCode.E) && !isSwordSwinging && !isShieldUp)
        {
            StartCoroutine(SwingSword());
        }

        // Hold the shield up when "Q" is held down
        if (Input.GetKey(KeyCode.Q))
        {
            if (!isShieldUp)
            {
                StartCoroutine(HoldShieldUp());
            }
        }
        else if (isShieldUp) // Reset shield when Q is released
        {
            StartCoroutine(ResetShield());
        }
    }

    IEnumerator SwingSword()
    {
        isSwordSwinging = true;
        canHit = true;  // Allow hitting during the swing

        // Set the sword's rotation to match the player's facing direction
        sword.transform.rotation = player.rotation;

        // Calculate the target rotation and position for the swing
        Quaternion targetRotation = Quaternion.Euler(swordSwingAngleX, swordSwingAngleY, swordSwingAngleZ);
        Vector3 targetPosition = swordInitialPosition + swordSwingOffset; // Fixed horizontal offset

        // Rotate and move the sword to the swing target smoothly
        float timeElapsed = 0;
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * swordSwingSpeed;
            sword.transform.localRotation = Quaternion.Slerp(swordInitialRotation, targetRotation, timeElapsed);
            sword.transform.localPosition = Vector3.Lerp(swordInitialPosition, targetPosition, timeElapsed);  // Smoothly move to the target position
            yield return null;
        }

        // Brief pause at the end of the swing
        yield return new WaitForSeconds(0.05f);

        // Return the sword to its initial position and rotation smoothly
        timeElapsed = 0;
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * swordSwingSpeed;
            sword.transform.localRotation = Quaternion.Slerp(targetRotation, swordInitialRotation, timeElapsed);
            sword.transform.localPosition = Vector3.Lerp(targetPosition, swordInitialPosition, timeElapsed);  // Return to the initial position
            yield return null;
        }

        // End of sword swing
        isSwordSwinging = false;
        canHit = false;  // Disable hitting after the swing
    }

    IEnumerator HoldShieldUp()
    {
        isShieldUp = true;

        // Calculate the target position for the shield, maintaining a constant orientation
        Vector3 targetPosition = shieldInitialPosition + shieldSwingOffset;

        // Smoothly raise the shield without changing its orientation based on player rotation
        float timeElapsed = 0;
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * shieldSwingSpeed;
            // Set the shield rotation to a fixed angle while raising it
            shield.transform.localRotation = Quaternion.Euler(shieldUpAngleX, 0f, 0f);
            shield.transform.localPosition = Vector3.Lerp(shieldInitialPosition, targetPosition, timeElapsed);
            yield return null;
        }
    }

    IEnumerator ResetShield()
    {
        isShieldUp = false;

        // Reset the shield to its initial position and rotation smoothly
        float timeElapsed = 0;
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * shieldSwingSpeed;
            shield.transform.localRotation = Quaternion.Slerp(shield.transform.localRotation, shieldInitialRotation, timeElapsed);
            shield.transform.localPosition = Vector3.Lerp(shield.transform.localPosition, shieldInitialPosition, timeElapsed);
            yield return null;
        }
    }

    // Method to check if the sword can hit something
    public bool CanHit()
    {
        return canHit;
    }
}