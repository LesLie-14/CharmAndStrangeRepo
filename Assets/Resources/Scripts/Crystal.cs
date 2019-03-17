using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D collision) {
        //add crystal to player
        if(collision.tag  == "Player") {
            Elemento.crystals += 1;
        }
        Destroy(gameObject);
    }
}
