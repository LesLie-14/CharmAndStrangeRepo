using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    public GameObject explosion;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided");
           
            //Destroy(collision.gameObject);
            Elemento.hits = Elemento.hits - 1;
        }
        if (collision.gameObject.tag == "spell")
        {
          GameObject explosionAnimation = Instantiate(explosion,this.transform.position,this.transform.rotation);
          Destroy(explosionAnimation, 0.5f);
          Destroy(collision.gameObject);
          Destroy(this.gameObject);  
        }
    }
    
}
