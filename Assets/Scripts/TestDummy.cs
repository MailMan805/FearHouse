using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that hit this one has the tag "Needle"
        if (other.CompareTag("Needle"))
        {
            // Get the CharacterController component from the sword's parent or character object
            AgentPineControls AgentPineControls = other.GetComponentInParent<AgentPineControls>();

            // Only register the hit if the sword is currently swinging (canHit is true)
            if (AgentPineControls != null && AgentPineControls.CanHit())
            {
                // Print a message to the console
                Debug.Log("The needle hit the object while swinging!");
            }
        }
    }
}
