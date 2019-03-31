using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeball : MonoBehaviour
{

    Rigidbody2D body;
    bool leftdir = true;
    public float speed = 5.0f;

    bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        
        Physics.IgnoreLayerCollision(9, 9, true);
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Grounded();

        //System.Console.WriteLine(grounded);
        if(!grounded) {
            leftdir = !leftdir;
        } 

        if (leftdir) {
            body.velocity = new Vector2(-1, speed * body.velocity.y);
            this.transform.localScale= new Vector3(4,4,4);
         } else {
             body.velocity = new Vector2(1, speed * body.velocity.y);
            this.transform.localScale = new Vector3(-4, 4, 4);
        }
    }

    bool Grounded() {
        return (Physics2D.Linecast(transform.position, new Vector3(transform.position.x,  transform.position.y-1.2f,  transform.position.z),
            1 << LayerMask.NameToLayer("Ground")));
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag  == "Player") {
            Elemento.hits -= 1;
        }
    }
}
