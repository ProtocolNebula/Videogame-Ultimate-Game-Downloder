using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeControl : MonoBehaviour
{

    public Text time;


    // Update is called once per frame
    void Update()
    {
        //time.text = Time.time.ToString("0.0");
        time.text = System.DateTime.UtcNow.ToString("HH:mm");
    }
}
