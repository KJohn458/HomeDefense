using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_M : MonoBehaviour
{
    private Transform House;
    private int chargeTime = 3;
    private bool hasAttacked;
    private NavMeshAgent agent;
    private bool agentDestroyed;
    private GameObject HouseGameObj;
    private Health healthScript;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, House.position) <= 2)
        {
            Destroy(agent);
            agentDestroyed = true;
            transform.LookAt(House);
        }

        if (hasAttacked == false && agentDestroyed == true)
        {
            hasAttacked = true;
            Invoke("swingBranch", chargeTime);
        }

        //Debug.Log(Vector3.Distance(transform.position, House.position));
    }



    void swingBranch()
    {
        Debug.Log("Hit");
        hasAttacked = false;
        healthScript.Damage();
    }
}
