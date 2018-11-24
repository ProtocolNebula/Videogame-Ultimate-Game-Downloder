using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour {

    public string text;
    public GameObject element;

    public Popup(string text)
    {
        this.text = text;
    }

    public Popup(string text, GameObject element)
    {

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
