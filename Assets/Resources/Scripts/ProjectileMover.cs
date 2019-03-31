using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour {
    public float speed;
    private Rigidbody2D rbody;
    public GameObject explosion;
    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        if(GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Transform>().localScale.x == -4)
        {
            rbody.velocity = new Vector2(-1.0f * speed, 0);
        }
        else
        {
            rbody.velocity = new Vector2(1.0f * speed, 0);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            Debug.Log("Collided");
            /*if (collision.gameObject.tag == "enemy")
            {
                Instantiate(explosion, this.transform);
                Destroy(collision.gameObject);
            }*/
            
            if (collision.gameObject.tag != "Player")
           Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
