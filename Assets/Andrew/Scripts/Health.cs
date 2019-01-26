using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
	public int health;
	public Text text;
	public GameObject death;
    void Start()
    {
        health = 5;
		death.SetActive(false);
		Time.timeScale = 1f;
    }


    void Update()
    {
        text.text = health.ToString();
		if(health==0)
			gameover();
    }
	
	public void Damage()
	{
		health--;
	}
	
	public void gameover()
	{
		Time.timeScale = 0f;
		death.SetActive(true);
	}
}
