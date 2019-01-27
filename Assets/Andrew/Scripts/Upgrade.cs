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
	public Text build;
	private Health healthScript;
	private GameObject HouseGameObj;
	private Weapon weapon;
	private GameObject WeaponGameObj; 
	public Button speedButton;
	public Button powerButton;
	public Button woodButton;
	public Button buildButton;
	private PlayerControl control;
	public GameObject player;
	public int difficulty = 1;
	public bool woodMax;
	public bool powerMax;
	public bool speedMax;
	
	void Start()
	{
		speed.text = "Cost "+speedCost().ToString()+" wood";
		power.text = "Cost "+powerCost().ToString()+" wood";
		wood.text = "Cost "+woodCost().ToString()+" wood";
		build.text = "Cost "+buildCost().ToString()+" wood";
		HouseGameObj = GameObject.FindWithTag("House");
		WeaponGameObj = GameObject.FindWithTag("Weapon");
		healthScript = HouseGameObj.GetComponent<Health>();
		weapon = WeaponGameObj.GetComponent<Weapon>();
		control = player.GetComponent<PlayerControl>();
		woodMax = false;
		powerMax = false;
		speedMax = false;
	}
	
    void Update()
    {
		speed.text = "Cost "+speedCost().ToString()+" wood";
		power.text = "Cost "+powerCost().ToString()+" wood";
		wood.text = "Cost "+woodCost().ToString()+" wood";
		if (healthScript.wood<buildCost()){
			buildButton.interactable = false;
		}else{
			buildButton.interactable = true;
		}
		if (healthScript.wood<powerCost()||powerMax){
			powerButton.interactable = false;
		}else{
			powerButton.interactable = true;
		}
		if (healthScript.wood<speedCost()||speedMax){
			speedButton.interactable = false;
		}else{
			speedButton.interactable = true;
		}
		if (healthScript.wood<woodCost()||woodMax){
			woodButton.interactable = false;
		}else{
			woodButton.interactable = true;
		}
		
    }
	private int buildCost(){
		return upgrade_cost*2*difficulty;
	}
	
	private int speedCost(){
		return upgrade_cost*speed_level;
	}
	
	private int powerCost(){
		return upgrade_cost*power_level;
	}
	
	private int woodCost(){
		return upgrade_cost*wood_level;
	}
	
	public void Difficulty(){
		if (healthScript.wood>buildCost()){
			difficulty++;
			healthScript.Buy(buildCost());
		}
	}
	
	public void Power(){
		if (healthScript.wood>powerCost()){
			healthScript.Buy(powerCost());
			power_level++;
			weapon.damageIncrease();
			if (power_level>=3){
				powerMax = true;
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
				woodMax = true;
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
			control.moveSpeed+=.2f;
			if (speed_level>=3)
			{
				speedMax = true;
				speedButton.interactable = false;
			}
		}else{
			speedButton.interactable = false;
		}
	}
}
