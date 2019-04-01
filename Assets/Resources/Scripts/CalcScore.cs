using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CalcScore : MonoBehaviour
{
    //OBJ loaded
    public Text crystals;
    public Text time;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        crystals.text = "Crystals: " + Elemento.crystals;
        time.text = "Time: " + Elemento.time;
        int scoreVal = (int)(100 + Elemento.crystals * 10 + 300 - Math.Round(Elemento.time));
        Elemento.score += scoreVal;
        score.text = "Score: " + scoreVal;
        
    }
}
