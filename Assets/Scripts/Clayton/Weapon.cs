﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider BC;
	public int damage =1;

    void Start()
    {
        BC = GetComponent<BoxCollider>();
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

	public void damageIncrease()
	{
		damage++;
	}
    void OnTriggerCollider(Collider Enemy)
    {
        Debug.Log("Hit an enemy");
        if (Enemy.gameObject.tag == "Melee")
        {
			Debug.Log("Hit an enemy");
            Destroy(Enemy.gameObject);
        }
        if(Enemy.gameObject.tag == "Ranged")
        {
            Debug.Log("Hit an enemy");
            AI_R enemy_r = Enemy.gameObject.GetComponent<AI_R>();
            enemy_r.Hurt(damage);
        }

    }
}
