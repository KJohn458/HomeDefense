using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public int MoveSpeed;
    public Transform Player;
    public int minDist;
    public int maxDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);

        if(Vector3.Distance(transform.position, Player.position) >= minDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }


        if (Vector3.Distance(transform.position, Player.position) <= maxDist)
        {
            Debug.Log("Fire!");
        }
    }
}
