using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    Quaternion rotation;
    public float timeToSpawn;
    private float spawnTimer;

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
        minNum = 0.0f;
        maxNum = 10.0f;
    }

    void FixedUpdate()
    {
        rotation = gameObject.transform.rotation;

        countTimer();
    }

    void spawnMeleeDude()
    {
        GameObject clone;
        clone = Instantiate(meleeEnemy, transform.position, rotation);
        clone.GetComponent<AI_Melee>().enemyHealth = setMeleeHealth;
        clone.SetActive(true);

    }

    void spawnRangedDude()
    {
        GameObject clone;
        clone = Instantiate(rangedEnemy, transform.position, rotation);
        clone.GetComponent<AI_R>().enemyHealth = setRangedHealth;
        clone.SetActive(true);
    }

    void spawnRandomDude(float randomNum)
    {
        Debug.Log(randomNum);
        if (randomNum >= 5.0f)
        {
            spawnMeleeDude();
        }
        else
        {
            spawnRangedDude();
        }
    }

    void countTimer()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            if (spawnMelee && !spawnRanged)
            {
                spawnMeleeDude();
            }

            if (spawnRanged && !spawnMelee)
            {
                spawnRangedDude();
            }

            if (spawnMelee && spawnRanged)
            {
                randomNum = Random.Range(minNum, maxNum);
                spawnRandomDude(randomNum);
            }
            spawnTimer = timeToSpawn;
        }
    }
}
