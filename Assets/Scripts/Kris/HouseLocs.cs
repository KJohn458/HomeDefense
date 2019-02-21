using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLocs : MonoBehaviour
{
    public Transform HousePos;
    public GameObject HouseObj;
    public Transform Addon1Pos;
    public GameObject Addon1GO;
    public Transform Addon2Pos;
    public GameObject Addon2GO;
    public Transform Addon3Pos;
    public GameObject Addon3GO;

    public void Awake()
    {
        HouseObj = GameObject.FindGameObjectWithTag("House");
        HousePos = HouseObj.transform;
        Addon1GO = GameObject.FindGameObjectWithTag("Addon1");
        Addon1Pos = Addon1GO.transform;
        Addon2GO = GameObject.FindGameObjectWithTag("Addon2");
        Addon2Pos = Addon2GO.transform;
        Addon3GO = GameObject.FindGameObjectWithTag("Addon3");
        Addon3Pos = Addon3GO.transform;

        Addon1GO.SetActive(false);
        Addon2GO.SetActive(false);
        Addon3GO.SetActive(false);
    }
}
