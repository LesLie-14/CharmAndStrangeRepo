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

    // Start is called before the first frame update
    void Start()
    {
        charm = GameObject.Find("Charm");
        strange = GameObject.Find("Strange");
        cam = GameObject.Find("Camera");

        body = GetComponent<Rigidbody2D>();

        strange.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.X)) {
           charm.SetActive(!charm.activeSelf);
           strange.SetActive(!charm.activeSelf);
       }

       if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-0.1f, 0, 0);

       } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += new Vector3(0.1f, 0, 0);
       }

       if (Input.GetKeyDown(KeyCode.Space)) {
           body.AddForce(new Vector2(0, 60));
       }

    }

    void FixedUpdate(){
        cam.transform.position = new Vector3(gameObject.transform.position.x, cam.transform.position.y, cam.transform.position.z);
    }
}
