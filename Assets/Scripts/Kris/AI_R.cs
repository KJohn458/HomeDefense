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


    private int chargeTime = 3;
    public int distanceAwayFromHouse;
    private bool hasAttacked;
    public float bulletVelocity = 400f;
    public float EnemyDistanceRun = 50.0f;

    private bool findTarget;
    public int enemyHealth;

    Animator R;

    private void Start()
    {
        enemyHealth = 1;
        agent = GetComponent<NavMeshAgent>();
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        House = GameObject.FindGameObjectWithTag("House").transform;
        GameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        houseLocations = GameManagerObj.GetComponent<HouseLocs>();
        Player = GameObject.FindGameObjectWithTag("Player");
        findHouse();
        findTarget = true;
        col = GetComponent<Collider>();

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

        else if(Vector3.Distance(transform.position, houseToMoveTo.position) > distanceAwayFromHouse)
        {
           if(findTarget == false)
            {
                findHouse();
                findTarget = true;
            }
            agent.isStopped = false;
            healthScript = HouseGameObj.GetComponent<Health>();
            
            hasAttacked = false;
            agent = GetComponent<NavMeshAgent>();
            
        }


        else if(!hasAttacked)
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
        healthScript.Heal(1);
        Destroy(col);
        Destroy(agent);
        R.SetTrigger("Death");
        Invoke("deathAnim", 1.5f);
    }

    void deathAnim()
    {
        Destroy(gameObject);
    }

    void findHouse()
    {
        Debug.Log("I find the house too!");
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

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(houseToMoveTo.position, path);
        agent.destination = houseToMoveTo.position;
    }
}
