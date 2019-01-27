using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Melee : MonoBehaviour
{
    public int distanceAwayFromHouse;
    private bool agentStopped = false;
    private bool hasAttacked = false;
    public int swingTime = 3;
    public int enemyHealth;
	public int wood = 1;
    public int speedOfTree;
    public HouseLocs houseLocations;
    private Health healthScript;

    private GameObject HouseGameObj;
    private GameObject GameManagerObj;
    private Transform houseToMoveTo;
    private Transform House;
    private NavMeshAgent agent;

    Animator M;

    public void Start()
    {

        enemyHealth = 1;
        agent = GetComponent<NavMeshAgent>();
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        House = GameObject.FindGameObjectWithTag("House").transform;
        GameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        houseLocations = GameManagerObj.GetComponent<HouseLocs>();
        healthScript = HouseGameObj.GetComponent<Health>();
        findHouse();

        M = GetComponent<Animator>();
    }

    private void Update()
    {
        if (agent.speed > 0)
        {
            M.SetBool("isWalking", true);
        }
        else
        {
            M.SetBool("isWalking", false);
        }

        if (agent.speed == 0)
        {
            M.Play("Attack");
        }
        if(hasAttacked == false && agentStopped == true)
        {
            hasAttacked = true;
            Invoke("swingBranch", swingTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "House")
        {
            agent.speed = 0;
            agentStopped = true;
            transform.LookAt(houseToMoveTo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "House")
        {
            agentStopped = false;
            agent.speed = speedOfTree;
        }
    }

    void swingBranch()
    {

        Debug.Log("Hit");
        hasAttacked = false;
        healthScript.Damage(1);
        M.Play("Tree Attack");
    }

    public void Hurt(int amount)
    {
        Debug.Log("Test hurt");
        enemyHealth -= amount;
        Debug.Log(enemyHealth);
        if (enemyHealth == 0)
        {
            Death();
        }


    }

    public void Death()
    {
        healthScript.Heal(wood);
        Destroy(gameObject);
    }

    void findHouse()
    {
        Debug.Log("I find the house");
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
        }

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(houseToMoveTo.position, path);
        agent.destination = houseToMoveTo.position;
    }
}
