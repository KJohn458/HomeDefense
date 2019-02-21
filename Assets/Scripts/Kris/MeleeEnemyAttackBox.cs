using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttackBox : MonoBehaviour
{
    public MeshCollider MC;
    public int damage = 1;
    public bool hasAttacked;

    private Health healthScript;
    private PlayerHealth playerHealth;

    void Start()
    {
        healthScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Health>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        MC = GetComponent<MeshCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if((other.gameObject.tag == "House" || other.gameObject.tag == "Addon1" || other.gameObject.tag == "Addon2" || other.gameObject.tag == "Addon3") && hasAttacked == false)
        {
            healthScript.Damage(1);
            hasAttacked = true;
            MC.enabled = false ;
        }

        else if(other.gameObject.tag == "Player" && hasAttacked == false)
        {
            playerHealth.playerTakeDamage();
            hasAttacked = true;
            MC.enabled = false;
        }

        else
        {
            MC.enabled = false;
        }
    }

    public void turnOnCollider()
    {
        MC.enabled = true;
    }
}
