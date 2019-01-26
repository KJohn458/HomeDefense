﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
	public static bool paused = false;
	
	public GameObject pauseMenu;
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(paused)
			{
				Resume();
			}else{
				Pause();
			}
		}
	}
	
	
	public void Resume(){
		paused = false;
		Time.timeScale = 1f;
		pauseMenu.SetActive(false);
	}
	
	
	void Pause(){
		paused = true;
		Time.timeScale = 0f;
		pauseMenu.SetActive(true);
	}
	
	public void ReturnMenu(){
		paused = false;
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}
}
