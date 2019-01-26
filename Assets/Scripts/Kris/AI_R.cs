using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_R : MonoBehaviour
{
    public Transform House;
    public Transform Player;
    public Rigidbody projectile;
    public int MoveSpeed;
    public int getCloseToHouseDistance;
    public int closeEnoughToFireAtHouseRange;
    public int avoidDistance;


    private int chargeTime = 3;
    private int bulletSpeed = 6;
    private bool hasAttacked;


    // Start is called before the first frame update
    void Start()
    {
        hasAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(House);
        if(Vector3.Distance(transform.position, House.position) >= getCloseToHouseDistance)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        if(Vector3.Distance(transform.position, Player.position) >= avoidDistance)
        {
            //code to avoid player
        }

        if(Vector3.Distance(transform.position, House.position) <= closeEnoughToFireAtHouseRange && !hasAttacked)
        {
            hasAttacked = true;
            Invoke("fireBullet", chargeTime);
        }
      /* transform.LookAt(House);
            if (Vector3.Distance(transform.position, House.position) >= minDist && Vector3.Distance(transform.position, House.position) >= maxDist)
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            }

            if (Vector3.Distance(transform.position, House.position) >= maxDist && Vector3.Distance(transform.position, Player.position) >= avoidDistance && !hasAttacked)
            {
                hasAttacked = true;
                Invoke("fireBullet", chargeTime);
            }
            */
        }


    void fireBullet()
    {
        Debug.Log("fire!");
        Rigidbody instance = Instantiate(projectile);
        instance.velocity = transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        hasAttacked = false;
    }
}
