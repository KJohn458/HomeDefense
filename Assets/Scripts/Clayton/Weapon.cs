using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider SC;
	private AI_M enemy_m;
	private GameObject enemy_obj;

    void Start()
    {
        SC = GetComponent<BoxCollider>();
		enemy_obj = GameObject.FindWithTag("Enemy");
        enemy_m = enemy_obj.GetComponent<AI_M>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SC.enabled = true;
        }
        else
        {
            SC.enabled = false;
        }
		
    }
    void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.tag == "Enemy")
        {
            enemy_m.enemyHealth-=1;
			
        }
		if (enemy_m.enemyHealth<=0)
		{
			Destroy(Enemy.gameObject);
			Debug.Log("Kill");
		}
    }
}
