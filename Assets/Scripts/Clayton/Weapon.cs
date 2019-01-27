using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider BC;
    private AudioSource attackSound;
	public int damage =1;

    void Start()
    {
        BC = GetComponent<BoxCollider>();
        attackSound = GetComponent<AudioSource>();
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

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Melee")
        {
            Debug.Log("Hit melee");
            AI_Melee enemy_m = other.gameObject.GetComponent<AI_Melee>();
            enemy_m.Hurt(damage);
            attackSound.Play();

        }
        if (other.gameObject.tag == "Ranged")
        {
            Debug.Log("Hit ranged");
            AI_R enemy_r = other.gameObject.GetComponent<AI_R>();
            enemy_r.Hurt(damage);
            attackSound.Play();
        }
    }

}
