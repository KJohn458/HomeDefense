using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    private int HouseLvl;
    public Text houseLvl;
    public Text WinTxt;
    // Start is called before the first frame update
    void Start()
    {
        HouseLvl = 0;
        SethouseLvl ();
        WinTxt.text = "";
    }
    void SethouseLvl()
    {
        houseLvl.text = "Score: " + houseLvl.ToString();
        if (HouseLvl >= 3)
        {
            WinTxt.text = "YOU HAVE DEAFETED THE DARK WOODS";
        }
    }   
}
