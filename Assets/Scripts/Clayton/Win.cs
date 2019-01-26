using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    private int HouseLvl;
    public Text HP;
    public Text WinTxt;
    void Start()
    {
        HouseLvl = 1;
        
        WinTxt.text = "";
    }

    private void Update()
    {
        SethouseLvl();
    }

    void SethouseLvl()
    {
        if (HouseLvl >= 10)
        {
            WinTxt.text = "YOU HAVE DEAFETED THE DARK WOODS";
        }
        else if (HouseLvl == 0)
        {
            WinTxt.text = "You Have Been Defeated!";
        }
    }   
}
