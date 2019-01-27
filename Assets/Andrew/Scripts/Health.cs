using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
	public int health;
	public int wood;
	public Text HP;
	public Text Wood;
	public GameObject Death;
	public GameObject Win;

    public GameObject addonOne;
    public GameObject addonTwo;
    public GameObject addonThree;

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
	
	public int mod = 1;

    public Upgrade upgradeScript;


    void Start()
    {
        spawn1.SetActive(true);
        spawn2.SetActive(false);
        spawn3.SetActive(false);
        spawn4.SetActive(false);
        health = 5;
		wood = 0;
		Death.SetActive(false);
        Time.timeScale = 1f;

    }


    void Update()
    {
        HP.text = health.ToString();
		Wood.text = wood.ToString();
		
		if(health==0)
        {
            gameover();
        }

        if(upgradeScript.difficulty == 0)
        {
            spawn1.SetActive(true);
            spawn2.SetActive(false);
            spawn3.SetActive(false);
            spawn4.SetActive(false);

            addonOne.SetActive(false);
        }

        if(upgradeScript.difficulty == 1)
        {
            spawn1.SetActive(true);
            spawn2.SetActive(true);
            spawn3.SetActive(false);
            spawn4.SetActive(false);

            addonOne.SetActive(true);
            addonTwo.SetActive(false);
        }

        if (upgradeScript.difficulty == 2)
        {
            spawn1.SetActive(true);
            spawn2.SetActive(true);
            spawn3.SetActive(true);
            spawn4.SetActive(false);

            addonOne.SetActive(true);
            addonTwo.SetActive(true);
            addonThree.SetActive(false);
        }

        if (upgradeScript.difficulty == 3)
        {
            spawn1.SetActive(true);
            spawn2.SetActive(true);
            spawn3.SetActive(true);
            spawn4.SetActive(true);

            addonOne.SetActive(true);
            addonTwo.SetActive(true);
            addonThree.SetActive(true);
        }

        if(upgradeScript.difficulty >= 4)
        {
			winGame();
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

	public void winGame()
	{
		Time.timeScale = 0f;
		Win.SetActive(true);
	}
	public void Buy(int amount)
	{
		wood-=amount;
	}
	
	public void Gather(int amount){
		wood+=1*mod;
	}

    public void Heal(int amount)
    {
        health+=amount;
    }

    public void spendWood(int amount)
    {
        wood = wood - amount;
    }
}
