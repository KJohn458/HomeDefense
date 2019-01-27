using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private int upgrade_cost = 15;
	private int reach_level = 1;
	private int power_level = 1;
	private int wood_level = 1;
	public Text reach;
	public Text power;
	public Text wood;
	private Health healthScript;
	private GameObject HouseGameObj;
	private Weapon weapon;
	private GameObject WeaponGameObj; 
	public GameObject reachLv1;
	public GameObject reachLv2;
	public GameObject reachLv3;
	public Button reachButton;
	public Button powerButton;
	public Button woodButton;
	void Start()
	{
		reach.text = "Cost "+reachCost().ToString()+" wood";
		power.text = "Cost "+powerCost().ToString()+" wood";
		wood.text = "Cost "+woodCost().ToString()+" wood";
		HouseGameObj = GameObject.FindWithTag("House");
		WeaponGameObj = GameObject.FindWithTag("Weapon");
		healthScript = HouseGameObj.GetComponent<Health>();
		weapon = WeaponGameObj.GetComponent<Weapon>();
	}
	
    void Update()
    {
		reach.text = "Cost "+reachCost().ToString()+" wood";
		power.text = "Cost "+powerCost().ToString()+" wood";
		wood.text = "Cost "+woodCost().ToString()+" wood";
		if (healthScript.wood<powerCost()){
			powerButton.interactable = false;
		}else{
			powerButton.interactable = true;
		}
		if (healthScript.wood<reachCost()){
			powerButton.interactable = false;
		}else{
			powerButton.interactable = true;
		}
		if (healthScript.wood<woodCost()){
			powerButton.interactable = false;
		}else{
			powerButton.interactable = true;
		}
		
    }
	private int reachCost()
	{
		return upgrade_cost*reach_level;
	}
	private int powerCost()
	{
		return upgrade_cost*power_level;
	}
	private int woodCost()
	{
		return upgrade_cost*power_level;
	}
	
	public void Power()
	{
		if (healthScript.wood>powerCost()){
			healthScript.Damage(powerCost());
			power_level++;
			weapon.damageIncrease();
			if (power_level>=3){
				powerButton.interactable = false;
			}
		}else{
			powerButton.interactable = false;
		}
		
	}
	public void Wood()
	{
		if (healthScript.wood>powerCost()){
			healthScript.Damage(woodCost());
			wood_level++;
			healthScript.mod+=1;
			if (wood_level>=3)
			{
				woodButton.interactable = false;
			}
		}else{
			woodButton.interactable = false;
		}
	}
	public void Reach()
	{
		if (healthScript.wood>powerCost()){
			healthScript.Damage(reachCost());
			reach_level++;
			if (reach_level== 2)
			{
				reachLv1.SetActive(false);
				reachLv2.SetActive(true);
			}
			if (reach_level>=3)
			{
				reachLv2.SetActive(false);
				reachLv3.SetActive(true);
				reachButton.interactable = false;
			}
		}else{
			reachButton.interactable = false;
		}
	}
}
