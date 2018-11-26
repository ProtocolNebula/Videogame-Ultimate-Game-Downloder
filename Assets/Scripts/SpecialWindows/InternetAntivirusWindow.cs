using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InternetAntivirusWindow : MonoBehaviour {

    public int test = 0;
    private List<float> prices = new List<float> { 50, 90, 130 };
    private List<int> times = new List<int> { 20, 50, 110 };

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuyAntivirus(int id)
    {
        GameManager.instance.BuyAntivirus(
            prices[id], 
            times[id]
        );
    }
}
