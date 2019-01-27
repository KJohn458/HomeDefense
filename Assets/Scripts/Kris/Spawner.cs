using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    private Vector3 pos;
    Quaternion rotation;
    public int spawnTimer;
    private bool spawnDelayBool;

    // new stuff

    public int setMeleeHealth;
    public int setRangedHealth;
    public float setMeleeSpeed;
    public float setRangedSpeed;
    public bool spawnRanged;
    public bool spawnMelee;



    [SerializeField]
    private bool defaultStats;
    private float minNum;
    private float maxNum;
    private float randomNum;
    
    

    void Start()
    {
        defaultStats = true;
        spawnDelayBool = false;
        minNum = 0.0f;
        maxNum = 10.0f;
    }

    void Update()
    {
        pos = gameObject.transform.position;
        rotation = gameObject.transform.rotation;

        if(spawnMelee && !spawnDelayBool)
        {
            spawnDelayBool = true;
            Invoke("spawnMeleeDude", spawnTimer);
        }

        if(spawnRanged && !spawnDelayBool)
        {
            spawnDelayBool = true;
            Invoke("spawnRangedDude", spawnTimer);
        }

        if((spawnMelee && spawnRanged) && !spawnDelayBool)
        {
            spawnDelayBool = true;
            randomNum = Random.Range(minNum, maxNum);
            Invoke("spawnRandomDude", spawnTimer);
        }
    }

    void spawnMeleeDude()
    {
        GameObject clone;
        clone = Instantiate(meleeEnemy, pos, rotation);
        clone.SetActive(true);
        spawnDelayBool = false;
    }

    void spawnRangedDude()
    {
        GameObject clone;
        clone = Instantiate(rangedEnemy, pos, rotation);
        clone.SetActive(true);
        spawnDelayBool = false;
    }

    void randomSpawnDude()
    {
        if (randomNum >= 5.0f)
        {
            spawnMeleeDude();
        }
        else
        {
            spawnRangedDude();
        }
    }
}
