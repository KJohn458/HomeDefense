using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Health healthScript;
    private GameObject HouseGameObj;
    private bool hitOnce;
    void Start()
    {
        HouseGameObj = GameObject.Find("House");
        healthScript = HouseGameObj.GetComponent<Health>();
        hitOnce = false;
    }

    void Update()
    {
        Invoke("destroyPrefab", 5);    
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enters collision");
        if (other.tag == "House" && hitOnce == false)
        {
            Debug.Log("Enters tag");
            healthScript.Damage();
            Destroy(gameObject);
            hitOnce = true;

        }
        

    }

    void destroyPrefab()
    {
        Destroy(gameObject);
    }
}
