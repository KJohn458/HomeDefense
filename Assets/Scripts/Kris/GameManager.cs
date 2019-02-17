﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private AI_Melee[] meleeDudes;
    private AI_R[] rangedDudes;

    private Health healthScript;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        healthScript = GameObject.FindGameObjectWithTag("House").GetComponent<Health>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (healthScript.health == 0)
        {
            gameObject.SetActive(false);
            rangedDudes = Object.FindObjectsOfType(typeof(AI_R)) as AI_R[];
            meleeDudes = Object.FindObjectsOfType(typeof(AI_Melee)) as AI_Melee[];
            for (int i = 0; i < meleeDudes.Length; i++)
            {
                meleeDudes[i].SendMessage("findHouse");
                Debug.Log("We're in the target loop");
            }
            for (int i = 0; i < rangedDudes.Length; i++)
            {
                rangedDudes[i].SendMessage("findHouse");
            }
        }
    }
}
