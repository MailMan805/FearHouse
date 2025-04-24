using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    public int damagePerTick = 5;
    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name + " takes " + damagePerTick + " fire damage.");
        // Check if the collided object is the player
        if (other.CompareTag("Pine") || other.CompareTag("Racc"))
        {
            Debug.Log(other.name + " takes " + damagePerTick + " fire damage.");
            // Get the PlayerHealth component from the player object
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Deal damage to the player
                playerHealth.TakeDamage(damagePerTick);
                Debug.Log(other.name + " takes " + damagePerTick + " fire damage.");
            }
        }
    }
}
