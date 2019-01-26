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
			Debug.Log("Box collider true");
        }
        else
        {
            BC.enabled = false;
        }

		
    }
    void OnTriggerEnter(Collider Enemy)
    {  
        if (Enemy.gameObject.tag == "Enemy")
        {
			Debug.Log("collision enter");
			AI_M enemy_m = Enemy.gameObject.GetComponent<AI_M>();
			enemy_m.Hurt(damage);
        }

    }
}
