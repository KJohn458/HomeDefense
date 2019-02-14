using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLocs : MonoBehaviour
{
    public Transform Addon1Pos;
    public GameObject Addon1GO;
    public Transform Addon2Pos;
    public GameObject Addon2GO;
    public Transform Addon3Pos;
    public GameObject Addon3GO;

    public void Awake()
    {
        Addon1GO = GameObject.FindGameObjectWithTag("Addon1");
        Addon1Pos = GameObject.FindGameObjectWithTag("Addon1").transform;
        Addon2GO = GameObject.FindGameObjectWithTag("Addon2");
        Addon2Pos = GameObject.FindGameObjectWithTag("Addon2").transform;
        Addon3GO = GameObject.FindGameObjectWithTag("Addon3");
        Addon3Pos = GameObject.FindGameObjectWithTag("Addon3").transform;

        Addon1GO.SetActive(false);
        Addon2GO.SetActive(false);
        Addon3GO.SetActive(false);
    }
}
