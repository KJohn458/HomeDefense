using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    private int HouseLvl;
    public Text WinText;

    void Start()
    {
        HouseLvl = 1;
        WinText.text = "";
    }

    private void Update()
    {
        SethouseLvl();
    }

    void SethouseLvl()
    {
        if (HouseLvl >= 10)
        {
            WinText.text = "YOU HAVE DEAFETED THE DARK WOODS";
        }
        else if (HouseLvl == 0)
        {
            WinText.text = "You Have Been Defeated!";
        }
    }   
}
