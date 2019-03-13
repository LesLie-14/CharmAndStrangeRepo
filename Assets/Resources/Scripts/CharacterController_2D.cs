using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterController_2D : MonoBehaviour {


    public bool grounded = false; //To check if the player touched the ground
    public Transform groundedEnd; //Connect to the empty game object that touched the grond from the player

    Transform mainCameraTransform;
    
    Rigidbody2D m_rigidbody; 
    Animator m_Animator;
    Transform m_tran;

    private float h = 0;
    private float v = 0;

    public float MoveSpeed = 40;

    public SpriteRenderer[] m_SpriteGroup;

    public bool Once_Attack = false;

    public static GameObject deathLine; // For the failed message if falls

    // Use this for initialization
    void Start () {

        //To make the fail text hidden at start
        deathLine = GameObject.FindGameObjectWithTag("failmsg");
        deathLine.SetActive(false);
        
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_Animator = this.transform.Find("Swordsman_Model").GetComponent<Animator>();
        m_tran = this.transform;
        m_SpriteGroup = this.transform.Find("Swordsman_Model").GetComponentsInChildren<SpriteRenderer>(true);

        mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    
    // Update is called once per frame
    void Update () {

        Raycasting(); //Calling the Raycast method for line casting from player to ground

        spriteOrder_Controller();

        //To play the attack animation on mouse left click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Once_Attack = false;
            m_Animator.SetTrigger("Attack");
            m_rigidbody.velocity = new Vector3(0, 0, 0);
        }

        //To play the second type of attack animation on mouse right click
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Once_Attack = false;
            m_Animator.SetTrigger("Attack2");
            m_rigidbody.velocity = new Vector3(0, 0, 0);
        }


        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Die")||
            m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")|| m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            return;

        //Method for the character movement including jump
        Move_Fuc();

        //For horizontal and vertical movement
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        m_Animator.SetFloat("MoveSpeed", Mathf.Abs(h )+Mathf.Abs (v));
    }


    //To control the player sprite sorting order
    public int sortingOrder = 0;
    public int sortingOrderOrigine = 0;

    private float Update_Tic = 0;
    private float Update_Time = 0.1f;

    void spriteOrder_Controller()
    {

        Update_Tic += Time.deltaTime;

        if (Update_Tic > 0.1f)
        {
            sortingOrder = Mathf.RoundToInt(this.transform.position.y * 100);
            //Debug.Log("y::" + this.transform.position.y);
            //  Debug.Log("sortingOrder::" + sortingOrder);
            for (int i = 0; i < m_SpriteGroup.Length; i++)
            {

                m_SpriteGroup[i].sortingOrder = sortingOrderOrigine - sortingOrder;

            }

            Update_Tic = 0;
        }
    }

    // character Move Function
    void Move_Fuc()
    {
        //For left movement
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
          //  Debug.Log("Left");
            m_rigidbody.AddForce(Vector2.left * MoveSpeed);
            gameObject.transform.localScale = new Vector3(-4,
              gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            //if (B_FacingRight)
            //Filp();


        }

        //For right movement
        else if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
          //  Debug.Log("Right");
            m_rigidbody.AddForce(Vector2.right * MoveSpeed);
            gameObject.transform.localScale = new Vector3(4,
              gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            //if (!B_FacingRight)
            //    Filp();
        }

        //For up movement
        if (Input.GetKey(KeyCode.W))
        {
           // Debug.Log("up");
            m_rigidbody.AddForce(Vector2.up * MoveSpeed);
          
        }
        
        //For down movement
        if (Input.GetKey(KeyCode.S))
        {
           // Debug.Log("Down");
            m_rigidbody.AddForce(Vector2.down * MoveSpeed); 
            
        }

        //For jumping based on Raycasting
        //grounded variable is set by ray casting
        else if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            m_rigidbody.AddForce(new Vector2(0, 50), ForceMode2D.Impulse);
        }

    }


    // To flip the character sprite
    bool B_Attack = false;
    bool B_FacingRight = true;

    void Filp()
    {
        //B_FacingRight = !B_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;

        m_tran.localScale = theScale;
    }

    //   Raycasting method for jumping
    void Raycasting()
    {
        //Draw line from the origin of the player to the ground
        Debug.DrawLine(this.transform.position, groundedEnd.position, Color.green);

        //Sets the boolean variable based on collision 
        //Layer mask is used to ensure collision from only Ground layer objects
        grounded = Physics2D.Linecast(this.transform.position, groundedEnd.position, 1<<LayerMask.NameToLayer("Ground"));

    }
}
