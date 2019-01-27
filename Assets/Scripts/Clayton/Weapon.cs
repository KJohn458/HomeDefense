using System.Collections;
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

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Melee")
        {
            Debug.Log("Hit melee");
            AI_Melee enemy_m = other.gameObject.GetComponent<AI_Melee>();
            enemy_m.Hurt(damage);

        }
        if (other.gameObject.tag == "Ranged")
        {
            Debug.Log("Hit ranged");
            AI_R enemy_r = other.gameObject.GetComponent<AI_R>();
            enemy_r.Hurt(damage);
        }
    }

}
