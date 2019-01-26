using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private BoxCollider SC;

    void Start()
    {
        SC = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SC.enabled = true;
        }
        else
        {
            SC.enabled = false;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "trigger")
        {
            Destroy(col.gameObject);
        }
    }
}
