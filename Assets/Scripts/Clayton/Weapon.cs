using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider SC;
	private AI_M enemy_m;
	private GameObject enemy_obj;
	private bool hitOnce;
	public int damage = 1;

    void Start()
    {
        SC = GetComponent<BoxCollider>();
		enemy_obj = GameObject.FindWithTag("Enemy");
        enemy_m = enemy_obj.GetComponent<AI_M>();
		hitOnce = false;
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
		if (hitOnce = true)
			hitOnce = false;
		
    }
    void OnTriggerEnter(Collider Enemy)
    {  
        if (Enemy.gameObject.tag == "Enemy")
        {
            Destory(Enemy.gameObject);
        }

    }
}
