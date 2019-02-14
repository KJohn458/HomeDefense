using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Health healthScript;
    private GameObject HouseGameObj;
    private bool hitOnce;
    private float despawnTimer = 5.0f;
    void Start()
    {
        HouseGameObj = GameObject.FindGameObjectWithTag("House");
        healthScript = HouseGameObj.GetComponent<Health>();
        hitOnce = false;
    }

    void FixedUpdate()
    {
        countTimer();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "House" && hitOnce == false)
        {
            healthScript.Damage(1);
            Destroy(gameObject);
            hitOnce = true;
        }
        if (other.tag == "Addon1" && hitOnce == false)
        {
            healthScript.Damage(1);
            Destroy(gameObject);
            hitOnce = true;
        }
        if (other.tag == "Addon2" && hitOnce == false)
        {
            healthScript.Damage(1);
            Destroy(gameObject);
            hitOnce = true;
        }
        if (other.tag == "Addon3" && hitOnce == false)
        {
            healthScript.Damage(1);
            Destroy(gameObject);
            hitOnce = true;
        }

    }

    void countTimer()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}


