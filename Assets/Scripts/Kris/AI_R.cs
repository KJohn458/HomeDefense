using UnityEngine;
using UnityEngine.AI;

public class AI_R : MonoBehaviour
{
    // enemy ai and house finder objects
    private GameObject HouseGameObj; //for enemies to find the house
    public GameObject Player; // for the enemies to know where the player is
    private Transform houseToMoveTo; // transform that changes position based on nearby house
    public HouseLocs houseLocations; // gamemanager object that holds all possible houses
    private bool findTarget;
    private Collider col; // used to delete col when enemy killed to remove hitbox
    public int enemyAttackDistance;
    public float EnemyDistanceRun = 50.0f; // how close player has to be to enable running away

    private Health healthScript; // reference health script to add wood to currency
    private NavMeshAgent agent; // reference to navmeshagent to control movement

    //find current location of enemy
    private Vector3 pos;
    Quaternion rotation;

    //bullet stuff
    public GameObject bulletPrefab;
    public GameObject pivot; // where bullet fires from
    public float bulletVelocity = 400f;

    //attack and death
    private int chargeTime = 3;
    private bool hasAttacked;
    public int enemyHealth;

    // animation and sound
    Animator rangedAnims;
    new public AudioSource audio;
    public AudioClip deathAudioClip;
    public AudioClip attackAudioClip;
    public GameObject deathParticles;

    //loop stuff here
    private int houseLoop;
    private int numOfEvolutions; 

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<Collider>();
        audio = GetComponent<AudioSource>();
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        houseLocations = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HouseLocs>();
        Player = GameObject.FindGameObjectWithTag("Player");
        healthScript = HouseGameObj.GetComponent<Health>();
        findTarget = true;

        numOfEvolutions = 4;
        findHouse();
        rangedAnims = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //playing walking anim
        if (agent.speed > 0)
        {
            rangedAnims.SetBool("isWalking", true);
        }
        else
        {
            rangedAnims.SetBool("isWalking", false);
        }


        // instantiate position for bullet
        pos = pivot.transform.position;
        rotation = pivot.transform.rotation;

        // distance away from player currently
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        
        if (distance < EnemyDistanceRun && hasAttacked == false)
        {
            agent.isStopped = false;
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
            findTarget = false;
        }

        else if (Vector3.Distance(transform.position, houseToMoveTo.transform.position) > enemyAttackDistance)
        {
            if (findTarget == false)
            {
                findHouse();
                findTarget = true;
            }
            agent.isStopped = false;
            hasAttacked = false;
        }

        else if (!hasAttacked)
        {
            agent.isStopped = true;
            rangedAnims.Play("Tree Attack");
            hasAttacked = true;
            Invoke("rangedAttack", chargeTime);
        }
    }

    void rangedAttack()
    {
        transform.LookAt(houseToMoveTo.transform.position);
        GameObject clone;
        hasAttacked = false;
        clone = Instantiate(bulletPrefab, pos, rotation) as GameObject;
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity, ForceMode.Impulse);
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
        healthScript.Gather(1);
        Destroy(col);
        agent.speed = 0;
        rangedAnims.SetTrigger("Death");
        audio.PlayOneShot(deathAudioClip, 0.4f);
        deathParticles.SetActive(true);
        Invoke("deathAnim", 1.5f);
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

        for (houseLoop = 0; houseLoop < numOfEvolutions; houseLoop++)
        {
            Debug.Log("Looking for what house to move to");
            if (gameObjectArray[houseLoop].activeSelf == true)
            {
                if (Vector3.Distance(transform.position, transformArray[houseLoop].position) < Vector3.Distance(transform.position, houseToMoveTo.position))
                {
                    houseToMoveTo = transformArray[houseLoop];
                    houseToMoveTo.position = transformArray[houseLoop].position;
                    Debug.Log("Sets new pos for house to move to");
                }
            }
            else if(gameObjectArray[0].activeSelf == false)
            {
                houseToMoveTo = Player.transform;
                houseToMoveTo.position = Player.transform.position;
                EnemyDistanceRun = 0;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(pos, EnemyDistanceRun);
    }
}
