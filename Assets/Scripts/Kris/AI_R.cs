using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_R : MonoBehaviour
{
    private GameObject HouseGameObj;
    private Transform House;
    private Health healthScript;
    private NavMeshAgent agent;
    public GameObject Player;
    

    
    private int chargeTime = 3;
    private bool hasAttacked;
    private Rigidbody cloneRB;
    public GameObject bulletPrefab;
    public int distanceAwayFromHouse;
    public float bulletVelocity = 8000f;
    




    private Vector3 pos;
    Quaternion rotation;

    public float EnemyDistanceRun = 4.0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        HouseGameObj = GameObject.Find("House");
        House = GameObject.FindWithTag("House").transform;
    }

    private void Update()
    {
        pos = gameObject.transform.position;
        rotation = gameObject.transform.rotation;
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < EnemyDistanceRun && hasAttacked == false)
        {
            agent.isStopped = false;
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
        }

        else if(Vector3.Distance(transform.position, House.position) > distanceAwayFromHouse)
        {
            agent.isStopped = false;
            healthScript = HouseGameObj.GetComponent<Health>();
            
            hasAttacked = false;
            agent = GetComponent<NavMeshAgent>();
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(House.position, path);
            agent.destination = House.position;
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                Destroy(this);
            }
        }


        else if(!hasAttacked)
        {
            agent.isStopped = true;
            transform.LookAt(House);
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
}
