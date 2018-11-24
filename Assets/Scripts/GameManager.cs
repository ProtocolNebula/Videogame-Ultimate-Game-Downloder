using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.

    public List<GameObject> icons;

    public GameObject iconPrefab;
    public GameObject internetTask, torrentTask;

    public GameObject internetWindow;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //Update is called every frame.
    void Update()
    {

    }

    public void IconAction(int id)
    {
        if (id == 2)
            if (!internetTask.activeSelf)
            {
                internetWindow.GetComponent<Animator>().SetBool("isOpen", true);
                internetTask.SetActive(true);
            }
                
        if(id == 3)
            if (!torrentTask.activeSelf)
                torrentTask.SetActive(true);

        if (id > 4)
        {

        }
    }

    public void MinimizeWindow(int id)
    {
        if (id == 0)
        {
            Animator window = internetWindow.GetComponent<Animator>();
            window.SetBool("isOpen", !window.GetBool("isOpen")); 
        }

        if (id == 1)
        {
            //Animator window = torrentWindow.GetComponent<Animator>();
            //window.SetBool("isOpen", !window.GetBool("isOpen"));
        }

    }

    public void CloseWindow(int id)
    {
        if (id == 0)
        {
            Animator window = internetWindow.GetComponent<Animator>();
            window.SetBool("isOpen", !window.GetBool("isOpen"));
            internetTask.SetActive(false);
        }

        if (id == 1)
        {
            //Animator window = torrentWindow.GetComponent<Animator>();
            //window.SetBool("isOpen", !window.GetBool("isOpen"));
            torrentTask.SetActive(false);
        }
    }

    public void EndGame()
    {

    }
}
