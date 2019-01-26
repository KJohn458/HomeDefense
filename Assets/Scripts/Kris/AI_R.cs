using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_R : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isFiring;
    public GameObject Player;

    private Transform House;
    private int chargeTime = 3;
    private bool hasAttacked;
    private bool agentDestroyed;
    private GameObject HouseGameObj;
    private Health healthScript;

    public float EnemyDistanceRun = 4.0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isFiring = false;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < EnemyDistanceRun && isFiring == false)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
        }

        else
        {
            HouseGameObj = GameObject.Find("House");
            healthScript = HouseGameObj.GetComponent<Health>();
            agentDestroyed = false;
            House = GameObject.FindWithTag("House").transform;
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


        if (Vector3.Distance(transform.position, House.position) <= 2)
        {
            transform.LookAt(House);

        }
    }
}
