﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.

    #region "Main components"
    public List<GameObject> icons;

    public GameObject iconPrefab;
    public GameObject popupPrefab;
    public GameObject popupPrefabRansomware;
    public GameObject popupsContainer;

    public List<GameObject> tasks;
    public List<GameObject> windows;

    private List<GameObject> popups;
    public VirusController virusController;

    public const float OriginalGameSpeed = 1;
    public static float money = 10000;
    public static int games = 0;
    public static int recordGames;
    public static float recordMoney;
    public int maxPopups = 20;

    /// <summary>
    /// game speed altered by virus and antivirus
    /// </summary>
    private float gameSpeed;
    #endregion

    public GameObject notificationPanel;
    public Text gamesText;

    /// <summary>
    /// Specifiy if a task have window
    /// TASKID: true/false
    /// </summary>
    public List<bool> tasksHaveWindow;

    private void Start()
    {
        virusController = new VirusController(this);
        popups = new List<GameObject>(maxPopups);
        gameSpeed = OriginalGameSpeed;
        virusController.forceNewVirus();
    }

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

    private void FixedUpdate()
    {
        virusController.FixedUpdate();
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


    /// <summary>
    /// Generate a new popup using popup settings if are available slots
    /// </summary>
    /// <param name="popup">Settings to generate the popup</param>
    /// <returns>Return true if poup instantiated</returns>
    public bool NewPopup(Popup popup)
    {
        if (popups.Count > maxPopups) { return false; }

        GameObject newPopup = Instantiate(popupPrefab, popupsContainer.transform);
        popups.Add(newPopup);
        newPopup.GetComponent<PopupWindowController>().applySettings(popup, this, newPopup);

        return true;
    }

    /// <summary>
    /// Destroy a popup instance (called from PopupWindowController)
    /// </summary>
    /// <param name="popup">Popup index</param>
    public void DestroyPopup(GameObject element)
    {
        Destroy(element);
        popups.Remove(element);
    }

    public void NoticeMe(string notice)
    {
        notificationPanel.GetComponent<Animator>().SetBool("isOpen", true);
        notificationPanel.transform.GetChild(0).GetComponent<Text>().text = notice;
    }

    public void UnnoticeMe()
    {
        notificationPanel.GetComponent<Animator>().SetBool("isOpen", false);
    }

    //esto no hace nada (de momento ;D)
    public void UpdateIcons()
    {
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(2);
    }

    #region "Alter player stats"
    public float GameSpeed
    {
        get
        {
            return gameSpeed;
        }

        set
        {
            gameSpeed = value;
        }
    }

    /// <summary>
    /// Increase or decrease money
    /// </summary>
    /// <param name="quantity">Quantity of amount to add/remove (-quantity)</param>
    /// <param name="force">If FALSE and have not enough money, will not decreased, if true, will leave to 0</param>
    /// <returns>Amount incremented/decreased (WITHOUT MINUS SYMBOL)</returns>
    public float incrementMoney(float quantity, bool force = false)
    {
        Debug.Log("Money updating| Modifier: " + quantity + " | Current amount: " + money);
        // Will be decreased
        if (quantity < 0)
        {
            // Quantity in positive number to modify
            quantity = -quantity;

            // Not have enough money
            if (quantity >= money)
            {
                if (!force) { return 0; }

                // Force to available amount
                // Currently user can have negative money (debts)
                //quantity = money;
            }

            // Decrease money
            money -= quantity;
        }
        else
        {
            money += quantity;
        }


        Debug.Log("Money updated | Modifier: " + quantity + " | New amount: " + money);
        return quantity;
    }

    public void incrementGames(int quantity)
    {
        games += quantity;
        gamesText.text = "x " + games;
    }

    #endregion
}
