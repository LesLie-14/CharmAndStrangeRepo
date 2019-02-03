using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathlineScript : MonoBehaviour {

    GameObject deathLine; //for death if fall
    Animator animator; //to get the animator controller
    // Use this for initialization
    void Start () {
        animator = GameObject.Find("Swordsman_Model").GetComponent<Animator>();
        deathLine = GameObject.FindGameObjectWithTag("failmsg");
    }
	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            animator.SetBool("dead", true); //To play the death animation
            CharacterController_2D.deathLine.SetActive(true); //To make the failed text visible
        }
    }
}
