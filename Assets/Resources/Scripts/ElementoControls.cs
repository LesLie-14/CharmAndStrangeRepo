using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoControls : MonoBehaviour
{
    public float maxSpeed = 1.0f;
    public float jumpTakeOffSpeed = 1.0f;

    Rigidbody2D body;

    GameObject charm;
    GameObject strange;
    GameObject cam;

    //for the animator component
    Animator playerAnim;
    //to get the horizontal axis
    float hor;
    public float speed = 5;

    //For jump using ray casting
    bool grounded = true;
    Transform jumpPoint;

    // Start is called before the first frame update
    void Start()
    {
        charm = GameObject.Find("Charm");
        strange = GameObject.Find("Strange");
        cam = GameObject.Find("Camera");

        body = GetComponent<Rigidbody2D>();

        strange.SetActive(false);

        playerAnim = gameObject.GetComponent<Animator>();

        jumpPoint = GameObject.FindGameObjectWithTag("jumpPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Raycasting();
        hor = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(hor * speed, body.velocity.y);
        playerAnim.SetFloat("walk", Mathf.Abs(hor));

        if (hor < 0)
        {
            if (gameObject.transform.localScale.x != -4f)
            {
                gameObject.transform.localScale = new Vector3(-4,
               gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }

        }
        if (hor > 0)
        {
            if (gameObject.transform.localScale.x != 4f)
            {
                gameObject.transform.localScale = new Vector3(4f, 
                    gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }

        }

        if (Input.GetKeyDown(KeyCode.X)) {
           charm.SetActive(!charm.activeSelf);
           strange.SetActive(!charm.activeSelf);
       }

       if (Input.GetKeyDown(KeyCode.Space) && grounded == true) {
           body.AddForce(new Vector2(0, 60));
       }

    }

    void FixedUpdate(){
        cam.transform.position = new Vector3(gameObject.transform.position.x, 
            cam.transform.position.y, cam.transform.position.z);
    }

    //Raycast method for jumping to avoid infinite jumps
    void Raycasting()
    {
        Debug.DrawLine(this.transform.position, jumpPoint.transform.position, Color.red);

        grounded = Physics2D.Linecast(this.transform.position, jumpPoint.transform.position,
                   1 << LayerMask.NameToLayer("Ground"));
    }
}
