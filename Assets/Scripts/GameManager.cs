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

    public List<GameObject> tasks;
    public List<GameObject> windows;

    public VirusController virusController;

    public int numTorrents;
    public const float OriginalGameSpeed = 1;
    public static float money = 10000;
    public Text moneyContainer;

    [HideInInspector]
    public int currentGames = 0;

    // Total installed games in this play
    [HideInInspector]
    public static int games = 0;

    // Record in all play
    public static int recordGames;
    public static float recordMoney;
    public int maxGamesInstalled = 20;

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

    #region "Popup things"
    public int maxPopups = 20;

    public GameObject popupPrefab;
    public GameObject popupTrollPrefab;
    public GameObject popupRansomwarePrefab;
    public GameObject popupsContainer;
    public Sprite popupFullDiskImage;

    private List<GameObject> popups;
    #endregion

    private void Start()
    {
        virusController = new VirusController(this);
        popups = new List<GameObject>(maxPopups);
        gameSpeed = OriginalGameSpeed;
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
    /// <param name="force">If true, will add the popup even no space left</param>
    /// <returns>Return true if poup instantiated</returns>
    public bool NewPopup(Popup popup, bool force = false)
    {
        if (popups.Count > maxPopups) { return false; }

        GameObject prefabToUse;
        if (popup.isRansomware) { prefabToUse = popupRansomwarePrefab; }
        else if (popup.isTroll) { prefabToUse = popupTrollPrefab; }
        else { prefabToUse = popupPrefab; }

        GameObject newPopup = Instantiate(prefabToUse, popupsContainer.transform);
        popups.Add(newPopup);
        newPopup.GetComponent<PopupWindowController>().applySettings(popup, newPopup);

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

        updateMoneyText();
        Debug.Log("Money updated | Modifier: " + quantity + " | New amount: " + money);
        return quantity;
    }

    public void updateMoneyText()
    {
        moneyContainer.text = money.ToString() + " €";
    }

    public void incrementGames(int quantity)
    {
        games += quantity;
        gamesText.text = "x " + games;
    }


    /// <summary>
    /// check if are free space in disk to start a new download
    /// </summary>
    /// <param name="showPopup">If true and no space left, a popup will be shown</param>
    /// <returns></returns>
    public bool checkFreeSpace(bool showPopup = true)
    {
        Debug.Log("checking " + currentGames + " - " + maxGamesInstalled);
        if (currentGames >= maxGamesInstalled)
        {
            Debug.Log("out of space");
            // No space left
            Popup popup = new Popup().Randomize();
            popup.posX = 970;
            popup.posY = -420;
            //popup.imageRef = Resources.Load<Sprite>("GenericPopup/error_full_disk");
            popup.imageRef = popupFullDiskImage;
            NewPopup(popup, true);
            return false;
        }
        return true;
    }

    #endregion
}
