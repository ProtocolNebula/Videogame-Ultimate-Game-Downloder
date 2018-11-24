using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boringometerController : MonoBehaviour {

    private Image me;
    private float _currentTime;
    public float totalTime;

	// Use this for initialization
	void Start () {
        me = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        _currentTime += Time.deltaTime / totalTime;

        me.fillAmount = _currentTime;

        if (me.fillAmount >= 1)
        {
            GameManager.instance.EndGame();
        }
	}

    public void ReduceFiller(float amount)
    {
        me.fillAmount -= amount / totalTime;
    }
}
