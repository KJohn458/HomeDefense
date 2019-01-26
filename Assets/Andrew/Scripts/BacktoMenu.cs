using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoMenu : MonoBehaviour
{
    // Start is called before the first frame update
	public void Update(){
		if (Input.GetMouseButtonDown(0))
			SceneManager.LoadScene(0);
	}
}
	
