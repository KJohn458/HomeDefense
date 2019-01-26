using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_M : MonoBehaviour
{
    private Transform House;
    private int chargeTime = 3;
    private bool hasAttacked;
    private NavMeshAgent agent;
    private bool agentDestroyed;
    private GameObject HouseGameObj;
    private Health healthScript;
	public int enemyHealth;
    public int houseDistance;

    // Start is called before the first frame update
    void Start()
    {
		enemyHealth = 6;
        HouseGameObj = GameObject.FindWithTag("House");
        healthScript = HouseGameObj.GetComponent<Health>();
        agentDestroyed = false;
        House = GameObject.FindWithTag("House").transform;
        hasAttacked = false;
        agent = GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(House.position, path);
        agent.destination = House.position;     
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, House.position) <= houseDistance)
        {
            Destroy(agent);
            agentDestroyed = true;
            transform.LookAt(House);
        }
		if(hasAttacked == false && agentDestroyed == true)
        {
            hasAttacked = true;
            Invoke("swingBranch", chargeTime);
        }

        //Debug.Log(Vector3.Distance(transform.position, House.position));
    }
	
    void swingBranch()
    {
        Debug.Log("Hit");
        hasAttacked = false;
        healthScript.Damage();
    }
	
	public void Hurt(int amount){
		
		enemyHealth -= amount;
		Debug.Log(enemyHealth);
		if(enemyHealth==0)
			Death();
	}
	
	public void Death(){
		healthScript.Heal();
		Destroy(gameObject);
		
	}
}
