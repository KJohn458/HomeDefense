using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject meleeEnemy;
    public int spawnTimer;
    private bool spawnBool;
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
            Invoke("spawnMeleeDude", spawnTimer);
        }
    }

    void spawnMeleeDude()
    {
        GameObject clone;
        clone = Instantiate(meleeEnemy, pos, rotation);
        clone.SetActive(true);
        spawnBool = false;
    }
}
