using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public AudioSource audio;

    void  OnTriggerEnter2D(Collider2D collision) {
        //add crystal to player
        if(collision.tag  == "Player") {
            audio.Play();
            Elemento.crystals += 1;
            Destroy(gameObject);
        }
    }
}
