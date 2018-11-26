using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InternetAntivirusWindow : MonoBehaviour {

    public int test = 0;
    private List<float> prices = new List<float> { 100, 200, 300 };
    private List<int> times = new List<int> { 20, 30, 40 };

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
