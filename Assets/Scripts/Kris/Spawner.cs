using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    public int spawnTimer;
    public float percentOfMelee, minPercent, maxPercent;
    private bool spawnBool;
    private float randomNum;
    private Vector3 pos;
    Quaternion rotation;



    // Start is called before the first frame update
    void Start()
    {
        spawnBool = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = gameObject.transform.position;
        rotation = gameObject.transform.rotation;
        if (!spawnBool)
        {
            spawnBool = true;
            randomNum = Random.Range(minPercent, maxPercent);
            Invoke("spawnMeleeDude", spawnTimer);
            
}
    }

    void spawnMeleeDude()
    {
        GameObject clone;
        if(randomNum < percentOfMelee)
        {
            clone = Instantiate(meleeEnemy, pos, rotation);
        }
        else
        {
            clone = Instantiate(rangedEnemy, pos, rotation);
        }
        
        clone.SetActive(true);
        spawnBool = false;
    }
}
