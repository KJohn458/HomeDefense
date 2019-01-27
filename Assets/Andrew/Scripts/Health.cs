using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
	public int health;
	public Text text;
	public GameObject Death;

    public GameObject addonOne;

    public GameObject addonTwo;

    public GameObject addonThree;
	
	public int mod = 1;


    void Start()
    {
        health = 5;
		Death.SetActive(false);
        Time.timeScale = 1f;
    }


    void Update()
    {
        text.text = health.ToString();
		if(health==0)
        {
            gameover();
        }

        if(health < 15)
        {
            addonOne.SetActive(false);
        }

        if(health >= 15 && health < 30)
        {
            addonOne.SetActive(true);
            addonTwo.SetActive(false);
        }

        if (health >= 30 && health < 45)
        {
            addonOne.SetActive(true);
            addonTwo.SetActive(true);
            addonThree.SetActive(false);
        }

        if (health >= 45 && health < 60)
        {
            addonOne.SetActive(true);
            addonTwo.SetActive(true);
            addonThree.SetActive(true);
        }


    }
	
	public void Damage(int amount)
	{
		health-=amount;
	}
	
	public void gameover()
	{
		Time.timeScale = 0f;
		Death.SetActive(true);
	}

    public void Heal(int amount)
    {
        health+=amount*mod;
    }
}
