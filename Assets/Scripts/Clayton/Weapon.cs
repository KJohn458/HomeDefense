using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider BC;
	public int damage;

    void Start()
    {
        BC = GetComponent<BoxCollider>();
		damage = 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BC.enabled = true;
			//Debug.Log("Box collider true");
        }
        else
        {
            BC.enabled = false;
        }

		

    }

    void OnTriggerCollider(Collider Enemy)
    {
        Debug.Log("Hit an enemy");
        if (Enemy.gameObject.tag == "Melee")
        {
            AI_Melee enemy_m = Enemy.gameObject.GetComponent<AI_Melee>();
            Debug.Log("Hit an enemy");
            enemy_m.Hurt(damage);
            
        }
        if(Enemy.gameObject.tag == "Ranged")
        {
            Debug.Log("Hit an enemy");
            AI_R enemy_r = Enemy.gameObject.GetComponent<AI_R>();
            enemy_r.Hurt(damage);
        }

    }
}
