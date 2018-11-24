using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boringometerController : MonoBehaviour {

    private Image me;
    private float _currentTime;

    /// <summary>
    /// Total time in seconds
    /// </summary>
    public float totalTime;

	// Use this for initialization
	void Start () {
        me = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (me.fillAmount >= 1)
        {
            GameManager.instance.EndGame();
        }
        else
        {
            _currentTime += Time.deltaTime / totalTime;
            me.fillAmount = _currentTime;
        }
    }

    public void ReduceFiller(float amount)
    {
        me.fillAmount -= amount / totalTime;
    }
}
