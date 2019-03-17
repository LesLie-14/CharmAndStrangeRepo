using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour {

    TextMeshProUGUI timerText;
    private float startTime;
   // private bool finished = false;
    // Use this for initialization
    void Start () {
        timerText = GameObject.FindGameObjectWithTag("timer").GetComponent<TextMeshProUGUI>();
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

       // if (finished)   return;
        float t = Time.time - startTime;
        string min = ((int)t / 60).ToString().PadLeft(2, '0');
        string sec = (t % 60).ToString("f0").PadLeft(2, '0');

        timerText.SetText("TIME: " + min + " : " + sec);

	}

/*  
    public void Finish()
    {
        finished = true;
        timerText.color = Color.yellow;
    } */

    /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            this.Finish();
        }
    } */
}
