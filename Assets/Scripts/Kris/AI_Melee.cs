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
    private GameObject Player;
    private NavMeshAgent agent;
    public HouseLocs houseLocations;
    private Transform houseToMoveTo;
    private Health healthScript;
    private GameManager gameManager;
    private Collider col;

    //attacking 
    private bool hasAttacked = false;
    public float buildingAttackTimer = 3f;
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
    public bool HouseDestroyed;

    private float playerAttackTimer = 1f;

    private GameObject meleeAttackBox;


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        GameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        houseLocations = GameManagerObj.GetComponent<HouseLocs>();
        healthScript = GameManagerObj.GetComponent<Health>();
        gameManager = GameManagerObj.GetComponent<GameManager>();
        col = GetComponent<Collider>();
        Player = GameObject.FindGameObjectWithTag("Player");
        numOfEvolutions = 4;
        findHouse();
        audio = GetComponent<AudioSource>();
        meleeAnim = GetComponent<Animator>();

        meleeAttackBox = gameObject.transform.Find("HitBox").gameObject;

        HouseDestroyed = gameManager.HouseDestroyed;
    }

    private void FixedUpdate()
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
            if(HouseDestroyed == false)
            {
                attackTimer();
            }
            else
            {
                agent.speed = speedOfTree;
                agentStopped = false;
            }
        }
      

        if(HouseDestroyed == true)
        {
            houseToMoveTo = Player.transform;
            houseToMoveTo.position = Player.transform.position;
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(houseToMoveTo.position, path);
            agent.destination = houseToMoveTo.position;
        }

        if (HouseDestroyed == true && agentStopped == true)
        {
            attackTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "House" || other.gameObject.tag == "Addon1" || other.gameObject.tag == "Addon2" || other.gameObject.tag == "Addon3" )
        {
            Debug.Log("Stopping to Attack");
            agent.speed = 0;
            agent.velocity = Vector3.zero;
            agentStopped = true;
            transform.LookAt(houseToMoveTo);
        }
        else if(other.gameObject.tag == "Player" && HouseDestroyed == true)
        {
            Debug.Log("Stopping to Attack the Player");
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

        meleeAttackBox.GetComponent<MeleeEnemyAttackBox>().hasAttacked = false;
        meleeAttackBox.GetComponent<MeleeEnemyAttackBox>().MC.enabled = true;

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
        agent.speed = 0;
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

    void attackTimer()
    {
        if(HouseDestroyed == true)
        {
            playerAttackTimer -= Time.deltaTime;
        }
        else
        {
            buildingAttackTimer -= Time.deltaTime;
        }
        
        if (buildingAttackTimer <= 0 && hasAttacked == false)
        {
            hasAttacked = true;
            swingBranch();
            buildingAttackTimer = 3f;
            meleeAttackBox.GetComponent<MeleeEnemyAttackBox>().turnOnCollider();
        }
        else if(playerAttackTimer <= 0 && hasAttacked == false)
        {
            hasAttacked = true;
            swingBranch();
            playerAttackTimer = 1f;
            meleeAttackBox.GetComponent<MeleeEnemyAttackBox>().turnOnCollider();
        }
    }


    void findHouse()
    {
        GameObject[] gameObjectArray = { houseLocations.HouseObj, houseLocations.Addon1GO, houseLocations.Addon2GO, houseLocations.Addon3GO };
        Transform[] transformArray = { houseLocations.HousePos, houseLocations.Addon1Pos, houseLocations.Addon2Pos, houseLocations.Addon3Pos };
        houseToMoveTo = houseLocations.HouseObj.transform;
        houseToMoveTo.position = houseLocations.HouseObj.transform.position;

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
            else if (gameObjectArray[0].activeSelf == false)
            {
                houseToMoveTo = Player.transform;
                houseToMoveTo.position = Player.transform.position;
                HouseDestroyed = true;
                Debug.Log("Targeting the player");
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
