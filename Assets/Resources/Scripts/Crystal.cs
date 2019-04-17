using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    void  OnTriggerEnter2D(Collider2D collision) {
        //add crystal to player
        if(collision.tag  == "Player") {
            Elemento.crystals += 1;
            Destroy(gameObject);
        }
    }
}
