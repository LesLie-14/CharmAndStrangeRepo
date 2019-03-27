using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Elemento : MonoBehaviour
{
    public float maxSpeed = 1.0f;
    public float jumpTakeOffSpeed = 1.0f;

    Rigidbody2D body;
    Collider2D col;

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
    Transform jump0;
    Transform jump1;

    //crystals
    public static int crystals = 0;
    TextMeshProUGUI CrystalText;

    //hits
    public static int hits = 3;
    TextMeshProUGUI HitsText;

    // Start is called before the first frame update
    void Start()
    {
        charm = GameObject.Find("Charm");
        strange = GameObject.Find("Strange");
        cam = GameObject.Find("Camera");

        body = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        strange.SetActive(false);

        playerAnim = gameObject.GetComponent<Animator>();

        jump0 = GameObject.Find("Jump0").transform;
        jump1 = GameObject.Find("Jump1").transform;

        CrystalText = GameObject.Find("CrystalsText").GetComponent<TextMeshProUGUI>();
        HitsText = GameObject.Find("HitsText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Grounded();

        if (transform.position.y < -5.0 && hits > 0) 
         hits = 0;

        if(hits <= 0) {
            hits = 0;
            SceneManager.LoadScene(4);
        }

        hor = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(hor * speed, body.velocity.y);

        CrystalText.SetText("CRYSTALS: " + crystals);
        HitsText.SetText("HITS: " + hits);

        if (hor < 0)
        {
            if (gameObject.transform.localScale.x != -4f)
            {
                gameObject.transform.localScale = new Vector3(-4,
                    gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            playerAnim.SetBool("walk", true);
        }
        else if (hor > 0)
        {
            if (gameObject.transform.localScale.x != 4f)
            {
                gameObject.transform.localScale = new Vector3(4f, 
                    gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            playerAnim.SetBool("walk", true);
        } else {
            playerAnim.SetBool("walk", false);
        }

        if (Input.GetKeyDown(KeyCode.X) && crystals > 0) {
            if (crystals >= 10 && AtTear()) {
                crystals -= 10;
                SceneManager.LoadScene(4);
            } else if (crystals > 0) {
                charm.SetActive(!charm.activeSelf);
                strange.SetActive(!charm.activeSelf);
                crystals -= 1;
            }
        } 
        
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
             body.AddForce(new Vector2(0, 70));
        } 

    }

    void FixedUpdate(){
        cam.transform.position = new Vector3(gameObject.transform.position.x, 
            cam.transform.position.y, cam.transform.position.z);
    }

    //Raycast method for jumping to avoid infinite jumps
    bool Grounded()
    {
        Debug.DrawLine(this.transform.position, jump0.transform.position, Color.red);
        Debug.DrawLine(this.transform.position, jump1.transform.position, Color.red);

        return (Physics2D.Linecast(this.transform.position, jump0.transform.position,
                   1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(this.transform.position, jump1.transform.position,
                   1 << LayerMask.NameToLayer("Ground")));
    }

    bool AtTear() {
        return Physics2D.IsTouchingLayers(col, LayerMask.GetMask("Tear"));
    }
}
