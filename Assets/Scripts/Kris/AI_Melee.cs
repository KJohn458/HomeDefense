using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Melee : MonoBehaviour
{
    // See comments in AI_R to see what stuff here does
    //ai and house location stuff
    private GameObject HouseGameObj;
    private GameObject GameManagerObj;
    private NavMeshAgent agent;
    public HouseLocs houseLocations;
    private Transform houseToMoveTo;
    private Health healthScript;
    private Collider col;

    //attacking 
    private bool hasAttacked = false;
    public int swingTime = 3;
    public int enemyHealth;

    //movement
    public int speedOfTree;
    private bool agentStopped = false;

    // anim and sound stuff
    Animator meleeAnim;
    new public AudioSource audio;
    public AudioClip deathAudioClip;
    public AudioClip attackAudioClip;
    public GameObject deathParticles;

    //loop stuff here
    private int forLoop;
    private int numOfEvolutions;


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        GameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        houseLocations = GameManagerObj.GetComponent<HouseLocs>();
        healthScript = HouseGameObj.GetComponent<Health>();
        col = GetComponent<Collider>();

        numOfEvolutions = 4;
        findHouse();


        audio = GetComponent<AudioSource>();

        meleeAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (agent.speed > 0)
        {
            meleeAnim.SetBool("isWalking", true);
        }
        else
        {
            meleeAnim.SetBool("isWalking", false);
        }

        if (hasAttacked == false && agentStopped == true)
        {
            hasAttacked = true;
            Invoke("swingBranch", swingTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "House" || other.gameObject.tag == "Addon1" || other.gameObject.tag == "Addon2" || other.gameObject.tag == "Addon3")
        {
            Debug.Log("Yoyoyo");
            agent.speed = 0;
            agent.velocity = Vector3.zero;
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
            agent.velocity = Vector3.one;
        }
    }

    void swingBranch()
    {

        Debug.Log("Hit");
        hasAttacked = false;
        healthScript.Damage(1);
        audio.PlayOneShot(attackAudioClip, .4f);
        meleeAnim.Play("Tree Attack");
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
        healthScript.Gather(1);
        Destroy(agent);
        Destroy(col);
        meleeAnim.SetTrigger("Death");
        audio.PlayOneShot(deathAudioClip, 0.4f);
        deathParticles.SetActive(true);
        Invoke("deathAnim", 0.75f);

    }

    void deathAnim()
    {
        Destroy(gameObject);
    }


    void findHouse()
    {
        GameObject[] gameObjectArray = { HouseGameObj, houseLocations.Addon1GO, houseLocations.Addon2GO, houseLocations.Addon3GO };
        Transform[] transformArray = { HouseGameObj.transform, houseLocations.Addon1Pos, houseLocations.Addon2Pos, houseLocations.Addon3Pos };
        houseToMoveTo = HouseGameObj.transform;
        houseToMoveTo.position = HouseGameObj.transform.position;

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
