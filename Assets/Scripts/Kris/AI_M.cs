﻿using System;
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


    // Start is called before the first frame update
    void Start()
    {
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
        if (agent.remainingDistance == 0 && hasAttacked == false)
        {
            hasAttacked = true;
            Invoke("swingBranch", chargeTime);
        }
    }



    void swingBranch()
    {
        Debug.Log("Hit");
        hasAttacked = false;
    }
}
