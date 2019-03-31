using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    public Text objectiveText;
    float duration = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > duration)
        {
            objectiveText.enabled = false;
        }

        
            Color myColor = Color.white;
            float ratio = Time.time / duration;
            myColor.a = Mathf.Lerp(0, 1, ratio);
            objectiveText.color = myColor;
    }


}
