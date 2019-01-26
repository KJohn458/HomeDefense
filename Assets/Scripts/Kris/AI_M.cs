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

    private Transform houseToMoveTo;
    private Transform Addon1Pos;
    public GameObject Addon1GO;
    private Transform Addon2Pos;
    public GameObject Addon2GO;
    private Transform Addon3Pos;
    public GameObject Addon3GO;


    // Start is called before the first frame update
    void Start()
    {
        findHouse();
		enemyHealth = 6;
        HouseGameObj = GameObject.FindWithTag("House");
        healthScript = HouseGameObj.GetComponent<Health>();
        agentDestroyed = false;
        House = GameObject.FindWithTag("House").transform;
        hasAttacked = false;
        agent = GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(houseToMoveTo.position, path);
        agent.destination = houseToMoveTo.position;
        Addon1GO = GameObject.FindGameObjectWithTag("Addon1");
        Addon1Pos = GameObject.FindGameObjectWithTag("Addon1").transform;
        Addon2GO = GameObject.FindGameObjectWithTag("Addon2");
        Addon2Pos = GameObject.FindGameObjectWithTag("Addon2").transform;
        Addon3GO = GameObject.FindGameObjectWithTag("Addon3");
        Addon2Pos = GameObject.FindGameObjectWithTag("Addon3").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, houseToMoveTo.position) <= houseDistance)
        {
            Destroy(agent);
            agentDestroyed = true;
            transform.LookAt(houseToMoveTo);
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

    void findHouse()
    {

        if (Addon1GO.activeSelf == true && Addon2GO.activeSelf == false)
        {
            Debug.Log("test");
            if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, Addon1Pos.position))
            {
                houseToMoveTo = House;
                houseToMoveTo.position = House.position;
            }
            else
            {
                houseToMoveTo = Addon1Pos;
                houseToMoveTo.position = Addon1Pos.position;
            }
        }

        else if (Addon2GO.activeSelf == true && Addon3GO.activeSelf == false)
        {
            Debug.Log("test1");
            if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, Addon1Pos.position))
            {
                if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, Addon2Pos.position))
                {
                    houseToMoveTo = House;
                    houseToMoveTo.position = House.position;
                }
                else
                {
                    houseToMoveTo = Addon2Pos;
                    houseToMoveTo.position = Addon2Pos.position;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, Addon1Pos.position) < Vector3.Distance(transform.position, Addon2Pos.position))
                {
                    houseToMoveTo = Addon1Pos;
                    houseToMoveTo.position = Addon1Pos.position;
                }
                else
                {
                    houseToMoveTo = Addon2Pos;
                    houseToMoveTo.position = Addon2Pos.position;
                }
            }
        }

        else if (Addon3GO.activeSelf == true)
        {
            Debug.Log("test2");
            if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, Addon1Pos.position))
            {
                if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, Addon2Pos.position))
                {
                    if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, Addon3Pos.position))
                    {
                        houseToMoveTo = House;
                        houseToMoveTo.position = House.position;
                    }
                    else
                    {
                        houseToMoveTo = Addon3Pos;
                        houseToMoveTo.position = Addon3Pos.position;
                    }
                }
                else if (Vector3.Distance(transform.position, Addon2Pos.position) < Vector3.Distance(transform.position, Addon3Pos.position))
                {
                    houseToMoveTo = Addon2Pos;
                    houseToMoveTo.position = Addon2Pos.position;
                }

                else
                {
                    houseToMoveTo = Addon3Pos;
                    houseToMoveTo.position = Addon3Pos.position;
                }
            }
            else
            {
                Debug.Log("test3");
                if (Vector3.Distance(transform.position, Addon1Pos.position) < Vector3.Distance(transform.position, Addon2Pos.position))
                {
                    if (Vector3.Distance(transform.position, Addon1Pos.position) < Vector3.Distance(transform.position, Addon3Pos.position))
                    {
                        houseToMoveTo = Addon1Pos;
                        houseToMoveTo.position = Addon1Pos.position;
                    }
                    else
                    {
                        houseToMoveTo = Addon3Pos;
                        houseToMoveTo.position = Addon3Pos.position;
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, Addon2Pos.position) < Vector3.Distance(transform.position, Addon3Pos.position))
                    {
                        houseToMoveTo = Addon2Pos;
                        houseToMoveTo.position = Addon2Pos.position;
                    }
                    else
                    {
                        houseToMoveTo = Addon3Pos;
                        houseToMoveTo.position = Addon3Pos.position;
                    }
                }
            }
        }
        else
        {
            houseToMoveTo = House;
            houseToMoveTo.position = House.position;
            Debug.Log("lock on");
        }
    }
}
