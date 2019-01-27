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

    private Collider col;

    Animator M;

    new public AudioSource audio;
    public AudioClip deathAudioClip;
    public AudioClip attackAudioClip;

    public GameObject deathParticles;

    // new spawner stuff below
    private GameObject spawnerObj;
    private Spawner spawner;

    //loop stuff here
    private int forLoop;
    private int numOfEvolutions;


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        House = GameObject.FindGameObjectWithTag("House").transform;
        GameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        houseLocations = GameManagerObj.GetComponent<HouseLocs>();
        healthScript = HouseGameObj.GetComponent<Health>();
        col = GetComponent<Collider>();

        spawnerObj = GameObject.FindGameObjectWithTag("Spawner");
        spawner = spawnerObj.GetComponent<Spawner>();
        setHealthAndSpeed();
        numOfEvolutions = 4;
        findHouse();


        audio = GetComponent<AudioSource>();

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

        if (hasAttacked == false && agentStopped == true)
        {
            hasAttacked = true;
            Invoke("swingBranch", swingTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Yoyoyo");
        if (other.gameObject.tag == "House" || other.gameObject.tag == "Addon1" || other.gameObject.tag == "Addon2" || other.gameObject.tag == "Addon3")
        {

            agent.speed = 0;
            agentStopped = true;
            transform.LookAt(houseToMoveTo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "House" || other.gameObject.tag == "Addon1" || other.gameObject.tag == "Addon2" || other.gameObject.tag == "Addon3")
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
        audio.PlayOneShot(attackAudioClip, .4f);
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
        healthScript.Gather(wood);
        Destroy(agent);
        Destroy(col);
        M.SetTrigger("Death");
        audio.PlayOneShot(deathAudioClip, 0.4f);
        deathParticles.SetActive(true);
        Invoke("deathAnim", 1.5f);

    }

    void deathAnim()
    {
        Destroy(gameObject);
    }

    void setHealthAndSpeed()
    {
        enemyHealth = spawner.setMeleeHealth;
        agent.speed = spawner.setMeleeSpeed;
    }

    void findHouse()
    {
        GameObject[] gameObjectArray = { HouseGameObj, houseLocations.Addon1GO, houseLocations.Addon2GO, houseLocations.Addon3GO };
        Transform[] transformArray = { House, houseLocations.Addon1Pos, houseLocations.Addon2Pos, houseLocations.Addon3Pos };
        houseToMoveTo = House;
        houseToMoveTo.position = House.position;

        for (forLoop = 0; forLoop < numOfEvolutions; forLoop++)
        {
            Debug.Log("enters the olde fore loope");
            if (gameObjectArray[forLoop].activeSelf == true)
            {
                if (Vector3.Distance(transform.position, transformArray[forLoop].position) < Vector3.Distance(transform.position, houseToMoveTo.position))
                {
                    houseToMoveTo = transformArray[forLoop];
                    houseToMoveTo.position = transformArray[forLoop].position;
                    Debug.Log("Sets new pos for house to move to");
                }
            }
            else
            {
                break;
            }
        }

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(houseToMoveTo.position, path);
        agent.destination = houseToMoveTo.position;
    }
}
