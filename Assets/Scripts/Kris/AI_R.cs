using UnityEngine;
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

    public GameObject pivot;
    private Collider col;
    public GameObject deathParticles;

    public int wood;

    private int chargeTime = 3;
    public int distanceAwayFromHouse;
    private bool hasAttacked;
    public float bulletVelocity = 400f;
    public float EnemyDistanceRun = 50.0f;

    private bool findTarget;
    public int enemyHealth;

    Animator R;

    new public AudioSource audio;
    public AudioClip deathAudioClip;
    public AudioClip attackAudioClip;

    //loop stuff here
    private int forLoop;
    private int numOfEvolutions;




    private void Start()
    {

        wood = 1;
        agent = GetComponent<NavMeshAgent>();
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        House = GameObject.FindGameObjectWithTag("House").transform;
        GameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        houseLocations = GameManagerObj.GetComponent<HouseLocs>();
        Player = GameObject.FindGameObjectWithTag("Player");

        findTarget = true;
        col = GetComponent<Collider>();
        audio = GetComponent<AudioSource>();


        numOfEvolutions = 4;

        findHouse2();

        R = GetComponent<Animator>();
    }

    private void Update()
    {
        if (agent.speed > 0)
        {
            R.SetBool("isWalking", true);
        }
        else
        {
            R.SetBool("isWalking", false);
        }



        pos = pivot.transform.position;
        rotation = pivot.transform.rotation;
        float distance = Vector3.Distance(transform.position, Player.transform.position);


        if (distance < EnemyDistanceRun && hasAttacked == false)
        {
            agent.isStopped = false;
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
            findTarget = false;
        }

        else if (Vector3.Distance(transform.position, houseToMoveTo.position) > distanceAwayFromHouse)
        {
            if (findTarget == false)
            {
                findHouse2();
                findTarget = true;
            }
            agent.isStopped = false;
            healthScript = HouseGameObj.GetComponent<Health>();

            hasAttacked = false;
            agent = GetComponent<NavMeshAgent>();

        }


        else if (!hasAttacked)
        {
            agent.isStopped = true;
            R.Play("Tree Attack");
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
        transform.LookAt(houseToMoveTo);
        GameObject clone;
        hasAttacked = false;
        clone = Instantiate(bulletPrefab, pos, rotation) as GameObject;
        clone.AddComponent<Rigidbody>();
        cloneRB = clone.GetComponent<Rigidbody>();
        cloneRB.AddForce(transform.forward * bulletVelocity, ForceMode.Impulse);
        audio.PlayOneShot(attackAudioClip, 0.4f);

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
        Destroy(col);
        Destroy(agent);
        R.SetTrigger("Death");
        audio.PlayOneShot(deathAudioClip, 0.4f);
        deathParticles.SetActive(true);
        Invoke("deathAnim", 1.5f);
    }

    void deathAnim()
    {
        Destroy(gameObject);
    }



    void findHouse2()
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
