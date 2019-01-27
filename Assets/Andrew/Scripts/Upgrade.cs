using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public int upgrade_cost = 15;
	private int speed_level = 1;
	private int power_level = 1;
	private int wood_level = 1;
	public Text speed;
	public Text power;
	public Text wood;
	private Health healthScript;
	private GameObject HouseGameObj;
	private Weapon weapon;
	private GameObject WeaponGameObj; 
	public Button speedButton;
	public Button powerButton;
	public Button woodButton;
	private PlayerControl control;
	public GameObject player;
	public int difficalty = 0;
	
	void Start()
	{
		speed.text = "Cost "+speedCost().ToString()+" wood";
		power.text = "Cost "+powerCost().ToString()+" wood";
		wood.text = "Cost "+woodCost().ToString()+" wood";
		HouseGameObj = GameObject.FindWithTag("House");
		WeaponGameObj = GameObject.FindWithTag("Weapon");
		healthScript = HouseGameObj.GetComponent<Health>();
		weapon = WeaponGameObj.GetComponent<Weapon>();
		control = player.GetComponent<PlayerControl>();
	}
	
    void Update()
    {
		speed.text = "Cost "+speedCost().ToString()+" wood";
		power.text = "Cost "+powerCost().ToString()+" wood";
		wood.text = "Cost "+woodCost().ToString()+" wood";
		if (healthScript.wood<powerCost()){
			powerButton.interactable = false;
		}else{
			powerButton.interactable = true;
		}
		if (healthScript.wood<speedCost()){
			speedButton.interactable = false;
		}else{
			speedButton.interactable = true;
		}
		if (healthScript.wood<woodCost()){
			woodButton.interactable = false;
		}else{
			woodButton.interactable = true;
		}
		
    }
	private int buildCost(){
		return upgrade_cost*2*(difficalty+1);
	}
	
	private int speedCost(){
		return upgrade_cost*speed_level;
	}
	
	private int powerCost(){
		return upgrade_cost*power_level;
	}
	
	private int woodCost(){
		return upgrade_cost*power_level;
	}
	
	public void Difficalty(){
		if (healthScript.wood>buildCost()){
			difficalty++;
			healthScript.Buy(buildCost());
		}
	}
	
	public void Power(){
		if (healthScript.wood>powerCost()){
			healthScript.Buy(powerCost());
			power_level++;
			weapon.damageIncrease();
			if (power_level>=3){
				powerButton.interactable = false;
			}
		}else{
			powerButton.interactable = false;
		}	
	}
	
	public void Wood(){
		if (healthScript.wood>woodCost()){
			healthScript.Buy(woodCost());
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
	
	public void Speed(){
		if (healthScript.wood>speedCost()){
			healthScript.Buy(speedCost());
			speed_level++;
			//mod speed
			if (speed_level>=3)
			{
				speedButton.interactable = false;
			}
		}else{
			speedButton.interactable = false;
		}
	}
}
