using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer: MonoBehaviour
{
    public int maxHealth;
    public int attackDamage; //average attack damage
    private int currentHealth;
    private bool down = false;
    public void Hurt(int damage)
    {
        Debug.Log($"Oh shit! You just got bumped! Now your health is {currentHealth}");
        currentHealth = Math.Max(0,currentHealth - damage);
        if (currentHealth == 0)
        {
            down = true;
        }
    }
    public bool IsDown()
    {
        return down; 
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
    }
}
