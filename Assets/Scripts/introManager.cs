using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introManager : MonoBehaviour {

    public float time;

    private float _time;
    // Use this for initialization
    void Start() {
        GameManager.money = 10000;
        GameManager.games = 0;
    }

    // Update is called once per frame
    void Update() {
        _time += Time.deltaTime;

        if(_time > time)
        {
            SceneManager.LoadScene(1);
            _time = 0;
        }
    }
}
