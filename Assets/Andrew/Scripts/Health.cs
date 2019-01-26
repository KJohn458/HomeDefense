using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
	public int health;
	public Text text;
	public GameObject death;

    public GameObject addonOne;

    public GameObject addonTwo;

    public GameObject addonThree;


    void Start()
    {
        health = 5;
		death.SetActive(false);
        addonOne.SetActive(false);
        addonTwo.SetActive(false);
        addonThree.SetActive(false);
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
            addonTwo.SetActive(true);
            addonThree.SetActive(false);
        }

        if (health >= 45 && health < 60)
        {
            addonThree.SetActive(true);
        }


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

    public void Heal()
    {
        health++;
    }
}
