using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Health healthScript;
    private int playerHealth;
    private int maxHealth;

    private void Awake()
    {
        healthScript = GameObject.FindGameObjectWithTag("House").GetComponent<Health>();
        maxHealth = 3;
        playerHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerHealth <= 0)
        {
            healthScript.gameover();
        }
    }

    public void playerTakeDamage()
    {
        playerHealth--;
    }

    public void playerHeal()
    {
        if(playerHealth < maxHealth)
        {
            playerHealth++;
        }   
    }
}
