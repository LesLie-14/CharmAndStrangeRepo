using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoControls : MonoBehaviour
{

    GameObject charm;
    GameObject strange;

    // Start is called before the first frame update
    void Start()
    {
        charm = GameObject.Find("Charm");
        strange = GameObject.Find("Strange");

        strange.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.X)) {
           charm.SetActive(!charm.activeSelf);
           strange.SetActive(!charm.activeSelf);
       }
    }
}
