using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Elemento : MonoBehaviour
{
    public AudioSource [] sounds;

    //score
    public static int score = 0;

    //time
    public static float time = 0;

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
    bool isGrounded = true;
    bool isJumping = false;
    Transform jump0;
    Transform jump1;

    //crystals
    public static int crystals = 0;
    TextMeshProUGUI CrystalText;

    //level
    public static int level = 1;

    //Fore Shooting 
    public GameObject projectile;
    public float fireDelta = 0.5F;

    private float nextFire = 0.5F;
    private GameObject newProjectile;
    private float myTime = 0.0F;
    Transform spawnPoint;

    //hits
    public static int hits = 3;
    TextMeshProUGUI HitsText;

    // Start is called before the first frame update
    void Start()
    {
        crystals = 10;
        sounds = GetComponents<AudioSource>();

        charm = GameObject.Find("Charm");
        strange = GameObject.Find("Strange");
        cam = GameObject.Find("Camera");

        body = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        strange.SetActive(false);

        playerAnim = gameObject.GetComponent<Animator>();

        jump0 = GameObject.Find("Jump0").transform;
        jump1 = GameObject.Find("Jump1").transform;

        //Fire Spawn Point
        spawnPoint = GameObject.FindGameObjectWithTag("firespawn").GetComponent<Transform>();

        CrystalText = GameObject.Find("CrystalsText").GetComponent<TextMeshProUGUI>();
        HitsText = GameObject.Find("HitsText").GetComponent<TextMeshProUGUI>();

        //enemies and player don't physically collide with each other
        Physics2D.IgnoreLayerCollision(9, 9, true); 
        Physics2D.IgnoreLayerCollision(12, 9, true); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Shooting Fire Projectile
        myTime = myTime + Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButton("Fire1")) && myTime > nextFire && crystals > 0)
        {
            nextFire = myTime + fireDelta;
            Quaternion fireRotation;
            if (transform.localScale.x == -4)
            {
                fireRotation = Quaternion.Euler(0, 0, 270);
            }
            else
            {
                fireRotation = Quaternion.Euler(0, 0, 90);
            }
            newProjectile = Instantiate(projectile, spawnPoint.transform.position, fireRotation) as GameObject;
            newProjectile.GetComponent<AudioSource>().Play();

            Debug.Log("Fire");
            nextFire = nextFire - myTime;
            myTime = 0.0F;

            crystals -= 1;
        }

        isGrounded = Grounded();

        if (isJumping && isGrounded) {
            isJumping = false;
        }

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
                gameObject.transform.localScale = new Vector3(-4, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            playerAnim.SetBool("walk", true);
        }
        else if (hor > 0)
        {
            if (gameObject.transform.localScale.x != 4f)
            {
                gameObject.transform.localScale = new Vector3(4f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            playerAnim.SetBool("walk", true);
        } else {
            playerAnim.SetBool("walk", false);
        }

        if (!sounds[3].isPlaying && hor != 0 && isGrounded) {
            sounds[3].Play();
        } 
        
        if (sounds[3].isPlaying && (hor == 0 || !isGrounded || isJumping)) {
            sounds[3].Stop();
        }

        if (Input.GetKeyDown(KeyCode.X) && crystals > 0) {
            if (crystals >= 10 && AtTear()) {
                sounds[2].Play();
                crystals -= 10;
                level += 1;
                SceneManager.LoadScene(5);
            } else if (crystals > 0) {
                sounds[2].Play();
                charm.SetActive(!charm.activeSelf);
                strange.SetActive(!charm.activeSelf);
                crystals -= 1;
            }
        } 

        if ((Input.GetAxis("Jump") > 0 || Input.GetKeyDown(KeyCode.Space)) && isGrounded) {
            Debug.Log("Jump");
            //body.AddForce(new Vector2(0, 70));
            body.velocity = new Vector2(0, 14);
            isJumping = true;
            sounds[1].Play();
        }

        cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "enemy")
        {
           // Destroy(collision.gameObject);
            Elemento.hits -= 1;
            sounds[0].Play();
        }
    }

}
