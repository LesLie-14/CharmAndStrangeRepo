using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("xKeyText").GetComponent<TextMeshProUGUI>().enabled = false;
        GameObject.FindGameObjectWithTag("xkey").GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if (this.gameObject.tag == "xkey")
        {
            GameObject.FindGameObjectWithTag("xKeyText").GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        if(this.gameObject.tag == "xkey")
        {
            GameObject.FindGameObjectWithTag("xKeyText").GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
}
