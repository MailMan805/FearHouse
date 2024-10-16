using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPineControls : MonoBehaviour
{
    public GameObject sword;  // The sword object
    public Transform player;  // The player for rotation reference

    // Swing rotation angles for each axis
    public float swingAngleX = 0f;  // Swing rotation angle around the X axis
    public float swingAngleY = 90f;  // Swing rotation angle around the Y axis
    public float swingAngleZ = 0f;   // Swing rotation angle around the Z axis

    public float swingSpeed = 7f;   // Speed of the sword swing
    public Vector3 swingOffset = new Vector3(1f, 0f, 0f); // Position offset for horizontal swing

    private bool isSwinging = false; // Swing state check
    private bool canHit = false;     // Can hit flag during swing

    private Quaternion swordInitialRotation;  // Initial rotation of the sword
    private Vector3 swordInitialPosition;     // Initial position of the sword

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
    }

    void Update()
    {
        // Start swinging the sword when "E" is pressed
        if (Input.GetKeyDown(KeyCode.E) && !isSwinging)
        {
            StartCoroutine(SwingSword());
        }
    }

    IEnumerator SwingSword()
    {
        isSwinging = true;
        canHit = true;

        // Set the sword's rotation to match the player's facing direction
        sword.transform.rotation = player.rotation;

        // Calculate the target rotation and position for the swing
        Quaternion targetRotation = Quaternion.Euler(swingAngleX, swingAngleY, swingAngleZ);
        Vector3 targetPosition = swordInitialPosition + swingOffset; // Fixed horizontal offset

        // Rotate and move the sword to the swing target smoothly
        float timeElapsed = 0;
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * swingSpeed;
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
            timeElapsed += Time.deltaTime * swingSpeed;
            sword.transform.localRotation = Quaternion.Slerp(targetRotation, swordInitialRotation, timeElapsed);
            sword.transform.localPosition = Vector3.Lerp(targetPosition, swordInitialPosition, timeElapsed);  // Return to the initial position
            yield return null;
        }

        // End of swing
        isSwinging = false;
        canHit = false;
    }

    // Method to check if the sword is in a state to hit something
    public bool CanHit()
    {
        return canHit;
    }
}