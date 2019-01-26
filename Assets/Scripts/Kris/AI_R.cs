﻿using UnityEngine;
using UnityEngine.AI;

public class AI_R : MonoBehaviour
{
    private GameObject HouseGameObj;
    public GameObject Player;
    public GameObject bulletPrefab;

    private Health healthScript;
    private NavMeshAgent agent;

    private Vector3 pos;
    Quaternion rotation;

    private Rigidbody cloneRB;

    private Transform House;
    private Transform houseToMoveTo;

    private GameObject GameManagerObj;
    public HouseLocs houseLocations;


    private int chargeTime = 3;
    public int distanceAwayFromHouse;
    private bool hasAttacked;
    public float bulletVelocity = 8000f;
    public float EnemyDistanceRun = 4.0f;










    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        House = GameObject.FindGameObjectWithTag("House").transform;
        GameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        houseLocations = GameManagerObj.GetComponent<HouseLocs>();


    }

    private void Update()
    {
        pos = gameObject.transform.position;
        rotation = gameObject.transform.rotation;
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        findHouse();

        if (distance < EnemyDistanceRun && hasAttacked == false)
        {
            agent.isStopped = false;
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
        }

        else if(Vector3.Distance(transform.position, houseToMoveTo.position) > distanceAwayFromHouse)
        {
           
            agent.isStopped = false;
            healthScript = HouseGameObj.GetComponent<Health>();
            
            hasAttacked = false;
            agent = GetComponent<NavMeshAgent>();
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(houseToMoveTo.position, path);
            agent.destination = houseToMoveTo.position;
        }


        else if(!hasAttacked)
        {
            agent.isStopped = true;
            transform.LookAt(houseToMoveTo);
            hasAttacked = true;
            Invoke("rangedAttack", chargeTime);
        }

        else
        {
          //  Debug.Log("Charging my attack!");
        }
    }

    void rangedAttack()
    {
        GameObject clone;
        hasAttacked = false;
        clone = Instantiate(bulletPrefab, pos, rotation) as GameObject;
        clone.AddComponent<Rigidbody>();
        cloneRB = clone.GetComponent<Rigidbody>();
        cloneRB.AddForce(transform.forward * bulletVelocity, ForceMode.Impulse);
        
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
                    if(Vector3.Distance(transform.position, House.position) < Vector3.Distance(transform.position, houseLocations.Addon3Pos.position))
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
                else if(Vector3.Distance(transform.position, houseLocations.Addon2Pos.position) < Vector3.Distance(transform.position, houseLocations.Addon3Pos.position))
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
                    if(Vector3.Distance(transform.position, houseLocations.Addon1Pos.position) < Vector3.Distance(transform.position, houseLocations.Addon3Pos.position))
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
                    if(Vector3.Distance(transform.position, houseLocations.Addon2Pos.position) < Vector3.Distance(transform.position, houseLocations.Addon3Pos.position))
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
