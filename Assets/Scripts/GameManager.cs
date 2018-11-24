using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.

    public List<GameObject> icons;

    public GameObject iconPrefab;
    public List<GameObject> tasks;
    public List<GameObject> windows;

    public GameObject internetWindow;

    /// <summary>
    /// Specifiy if a task have window
    /// TASKID: true/false
    /// </summary>
    public List<bool> tasksHaveWindow;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //Update is called every frame.
    void Update()
    {

    }

    /// <summary>
    /// Launch a task at click on icon
    /// </summary>
    /// <param name="id">Icon ID (ref to taskID)</param>
    public void IconAction(int id)
    {
        GameObject element = tasks[id];

        if (!element.activeSelf) {
            element.SetActive(true);
            if (tasksHaveWindow[id])
            {
                GameObject elementWindow = windows[id];
                elementWindow.GetComponent<Animator>().SetBool("isOpen", true);
            }
        }
    }


    /// <summary>
    /// Close the window leaving task bar
    /// </summary>
    /// <param name="id"></param>
    public void MinimizeWindow(int id)
    {
        if (!tasksHaveWindow[id]) return;
        
        GameObject element = windows[id];

        if (element.activeSelf)
        {
            Animator window = element.GetComponent<Animator>();
            window.SetBool("isOpen", !window.GetBool("isOpen"));
        }
    }

    /// <summary>
    /// Close the window closing task bar icon
    /// </summary>
    /// <param name="id"></param>
    public void CloseWindow(int id)
    {
        if (!tasksHaveWindow[id]) return;
        
        GameObject element = windows[id];

        if (element.activeSelf)
        {
            // Close the windows
            Animator window = element.GetComponent<Animator>();
            window.SetBool("isOpen", !window.GetBool("isOpen"));

            // Close task in tasks bar
            GameObject elementTask = tasks[id];
            elementTask.SetActive(false);
        }
    }

    public void UpdateIcons()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    public void EndGame()
    {

    }
}
