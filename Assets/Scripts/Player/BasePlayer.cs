using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    private bool isDown = false;
    void Hurt(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isDown = true;
        }
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
    }
}
