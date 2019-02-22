using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInputManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(InputManager.instance.Pause())
        {
            Debug.Log("Pause");
        }

        if(InputManager.instance.Attack())
        {
            Debug.Log("Attack");
        }

        if(InputManager.instance.Sprint())
        {
            Debug.Log("Sprint");
        }
    }
}
