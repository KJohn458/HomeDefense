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
		Destroy(addonOne);
		Destroy(addonTwo);
		Destroy(addonThree);
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
            Destroy(addonOne);
        }

        if(health >= 15 && health < 30)
        {
            Instantiate(addonOne);
            Destroy(addonTwo);
        }

        if (health >= 30 && health < 45)
        {
            Instantiate(addonTwo);
            Destroy(addonThree);
        }

        if (health >= 45 && health < 60)
        {
            Instantiate(addonThree);
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
