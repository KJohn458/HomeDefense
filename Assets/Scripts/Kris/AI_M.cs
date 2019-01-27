using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_M : MonoBehaviour
{
    private bool hasAttacked;
    private bool agentDestroyed;
    private int chargeTime = 3;
	public int enemyHealth;
    public int houseDistance;
    
    public HouseLocs houseLocations;
    private Health healthScript;

    private GameObject HouseGameObj;
    private GameObject GameManagerObj;
    private Transform houseToMoveTo;
    private Transform House;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {

        enemyHealth = 1;
        
        healthScript = HouseGameObj.GetComponent<Health>();
        agentDestroyed = false;
        hasAttacked = false;

        agent = GetComponent<NavMeshAgent>();
        HouseGameObj = GameObject.FindWithTag("House");
        House = GameObject.FindWithTag("House").transform;

        GameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        houseLocations = GameManagerObj.GetComponent<HouseLocs>();

    }

    // Update is called once per frame
    void Update()
    {
        findHouse();
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(houseToMoveTo.position, path);
        agent.destination = houseToMoveTo.position;
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
        if (houseLocations.Addon1GO.activeSelf == true && houseLocations.Addon2GO.activeSelf == false)
        {
            Debug.Log("test");
            if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, houseLocations.Addon1Pos.position))
            {
                houseToMoveTo = House;
                houseToMoveTo.position = House.position;
            }
            else
            {
                houseToMoveTo = houseLocations.Addon1Pos;
                houseToMoveTo.position = houseLocations.Addon1Pos.position;
            }
        }

        else if (houseLocations.Addon2GO.activeSelf == true && houseLocations.Addon3GO.activeSelf == false)
        {
            Debug.Log("test1");
            if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, houseLocations.Addon1Pos.position))
            {
                if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, houseLocations.Addon2Pos.position))
                {
                    houseToMoveTo = House;
                    houseToMoveTo.position = House.position;
                }
                else
                {
                    houseToMoveTo = houseLocations.Addon2Pos;
                    houseToMoveTo.position = houseLocations.Addon2Pos.position;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, houseLocations.Addon1Pos.position) < Vector3.Distance(transform.position, houseLocations.Addon2Pos.position))
                {
                    houseToMoveTo = houseLocations.Addon1Pos;
                    houseToMoveTo.position = houseLocations.Addon1Pos.position;
                }
                else
                {
                    houseToMoveTo = houseLocations.Addon2Pos;
                    houseToMoveTo.position = houseLocations.Addon2Pos.position;
                }
            }
        }

        else if (houseLocations.Addon3GO.activeSelf == true)
        {
            Debug.Log("test2");
            if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, houseLocations.Addon1Pos.position))
            {
                if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, houseLocations.Addon2Pos.position))
                {
                    if (Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, houseLocations.Addon3Pos.position))
                    {
                        houseToMoveTo = House;
                        houseToMoveTo.position = House.position;
                    }
                    else
                    {
                        houseToMoveTo = houseLocations.Addon3Pos;
                        houseToMoveTo.position = houseLocations.Addon3Pos.position;
                    }
                }
                else if (Vector3.Distance(transform.position, houseLocations.Addon2Pos.position) < Vector3.Distance(transform.position, houseLocations.Addon3Pos.position))
                {
                    houseToMoveTo = houseLocations.Addon2Pos;
                    houseToMoveTo.position = houseLocations.Addon2Pos.position;
                }

                else
                {
                    houseToMoveTo = houseLocations.Addon3Pos;
                    houseToMoveTo.position = houseLocations.Addon3Pos.position;
                }
            }
            else
            {
                Debug.Log("test3");
                if (Vector3.Distance(transform.position, houseLocations.Addon1Pos.position) < Vector3.Distance(transform.position, houseLocations.Addon2Pos.position))
                {
                    if (Vector3.Distance(transform.position, houseLocations.Addon1Pos.position) < Vector3.Distance(transform.position, houseLocations.Addon3Pos.position))
                    {
                        houseToMoveTo = houseLocations.Addon1Pos;
                        houseToMoveTo.position = houseLocations.Addon1Pos.position;
                    }
                    else
                    {
                        houseToMoveTo = houseLocations.Addon3Pos;
                        houseToMoveTo.position = houseLocations.Addon3Pos.position;
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, houseLocations.Addon2Pos.position) < Vector3.Distance(transform.position, houseLocations.Addon3Pos.position))
                    {
                        houseToMoveTo = houseLocations.Addon2Pos;
                        houseToMoveTo.position = houseLocations.Addon2Pos.position;
                    }
                    else
                    {
                        houseToMoveTo = houseLocations.Addon3Pos;
                        houseToMoveTo.position = houseLocations.Addon3Pos.position;
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
