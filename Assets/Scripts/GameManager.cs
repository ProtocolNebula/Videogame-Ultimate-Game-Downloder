using System.Collections;
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

    private List<GameObject> popups;
    public VirusController virusController;

    public int numTorrents;
    public const float OriginalGameSpeed = 1;
    public static float money = 10000;
    [HideInInspector]
    public Text moneyContainer;

    [HideInInspector]
    public int currentGames = 0;
    public int maxInstalledGames = 20;

    // Total installed games in this play
    [HideInInspector]
    public static int games = 0;
    public static int recordGames;
    public static float recordMoney;

    /// <summary>
    /// game speed altered by virus and antivirus
    /// </summary>
    private float gameSpeed;
    #endregion

    #region "Popups"
    public GameObject popupPrefab;
    public GameObject popupTrollPrefab;
    public GameObject popupRansomwarePrefab;
    [HideInInspector]
    public GameObject popupsContainer;
    public Sprite popupError;
    public int maxPopups = 20;
    #endregion

    #region "Sound effects"
    private AudioSource audioManager;
    public AudioClip audioClick;
    public AudioClip audioNotification;
    public AudioClip audioError;
    public AudioClip audioPopup;
    #endregion

    [HideInInspector]
    public GameObject notificationPanel;
    [HideInInspector]
    public Text gamesText;

    /// <summary>
    /// Specifiy if a task have window
    /// TASKID: true/false
    /// </summary>
    public List<bool> tasksHaveWindow;

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
        if (level == 2)
        {
            FindInstences();
        }
    }

    private void Start()
    {
        //FindInstences();

        virusController = new VirusController(this);
        popups = new List<GameObject>(maxPopups);
        gameSpeed = OriginalGameSpeed;

        audioManager = gameObject.AddComponent<AudioSource>();
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
        // On any click, reproduce click sound
        if (Input.GetButtonDown("Fire1"))
        {
            audioManager.PlayOneShot(audioClick, 1.0f);
        }
    }

    private void FixedUpdate()
    {
        virusController.FixedUpdate();
    }

    /// <summary>
    /// Purchase antivirus (even if player have no money)
    /// If antivirus is already in computer, user can't purcahse it
    /// </summary>
    /// <param name="price">Antivirus cost</param>
    /// <param name="time">Duration time</param>
    /// <returns></returns>
    public void BuyAntivirus(float price, float time)
    {
        if (!virusController.areAntivirusRunning())
        {
            incrementMoney(-price, true);
            virusController.addAntivirus(time);
            NoticeMe("Antivirus: " + time + " segundos por " + price + " €");
            return;
        }

        NoticeMe("Ya hay un antivirus activo.");
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

            if (window.GetBool("isOpen"))
            {
                if (element.transform.GetSiblingIndex() == 1)
                {
                    window.SetBool("isOpen", !window.GetBool("isOpen"));
                    //element.transform.SetSiblingIndex(0);
                }
                else
                {
                    element.transform.SetSiblingIndex(1);
                }
            }
            else
            {
                window.SetBool("isOpen", !window.GetBool("isOpen"));
                element.transform.SetSiblingIndex(1);
            }
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

            // Close task in tasks barg
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

        GameObject prefabToUse;
        if (popup.isRansomware) { prefabToUse = popupRansomwarePrefab; }
        else if (popup.isTroll) { prefabToUse = popupTrollPrefab; }
        else { prefabToUse = popupPrefab; }

        GameObject newPopup = Instantiate(prefabToUse, popupsContainer.transform);
        popups.Add(newPopup);
        newPopup.GetComponent<PopupWindowController>().applySettings(popup, newPopup);

        if (popup.isError)
        {
            audioManager.PlayOneShot(audioError, 1.0f);
        }
        else
        {
            audioManager.PlayOneShot(audioPopup, 1.0f);
        }

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
        audioManager.PlayOneShot(audioNotification, 1.0f);
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

    public void FindInstences()
    {
        GameObject icon_shortcuts = GameObject.Find("icon_shortcuts");
        for (int i = 0; i < icon_shortcuts.transform.childCount; i++)
        {
            icons.Add(icon_shortcuts.transform.GetChild(i).gameObject);
        }

        GameObject task_bar = GameObject.Find("task_bar");
        for (int i = 0; i < task_bar.transform.childCount; i++)
        {
            tasks.Add(task_bar.transform.GetChild(i).gameObject);
        }

        GameObject Windows = GameObject.Find("Windows");
        for (int i = 0; i < Windows.transform.childCount - 1; i++)
        {
            windows.Add(Windows.transform.GetChild(i).gameObject);
        }

        moneyContainer = GameObject.Find("money_text").GetComponent<Text>();
        popupsContainer = GameObject.Find("popups_container");
        notificationPanel = GameObject.Find("notification_panel");
        gamesText = GameObject.Find("gamesCount_text").GetComponent<Text>();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(3);
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
        currentGames += quantity;
        gamesText.text = "x " + games;
    }

    public bool checkFreeSpace()
    {
        if (currentGames >= maxInstalledGames)
        {
            Popup popup = new Popup();
            popup.imageRef = popupError;
            popup.posX = 950;
            popup.posY = - 450;
            popup.isError = true;
            NewPopup(popup);
            return false;
        }
        return true;
    }

    #endregion
}
