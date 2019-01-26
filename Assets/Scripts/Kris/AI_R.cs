using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_R : MonoBehaviour
{
    public Transform House;
    public Transform Player;
    public Rigidbody projectile;
    public int avoidDistance;

    private NavMeshAgent agent;
    private int chargeTime = 3;
    private int bulletSpeed = 6;
    private bool hasAttacked;


    // Start is called before the first frame update
    void Start()
    {
        hasAttacked = false;
        House = GameObject.FindWithTag("House").transform;
        agent = GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(House.position, path);
        agent.destination = House.position;
        if (path.status == NavMeshPathStatus.PathPartial)
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance == avoidDistance && hasAttacked == false)
        {
            hasAttacked = true;
            Invoke("swingBranch", chargeTime);
        }
    }


    void fireBullet()
    {
        Debug.Log("fire!");
        Rigidbody instance = Instantiate(projectile);
        instance.velocity = transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        hasAttacked = false;
    }
}
