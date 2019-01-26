using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
	public int health;
	public Text text;

    void Start()
    {
        health = 5;
    }


    void Update()
    {
        text.text = health.ToString();
    }
	
	public void Damage()
	{
		health--;
	}
}
