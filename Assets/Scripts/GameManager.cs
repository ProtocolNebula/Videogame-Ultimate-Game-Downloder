using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.

    #region "Main components"
    public List<GameObject> icons;

    public GameObject iconPrefab;
    public List<GameObject> tasks;
    public List<GameObject> windows;

    private List<Popup> popups;
    public VirusController virusController;

    public const float OriginalGameSpeed = 1;
    public float money = 10000;

    /// <summary>
    /// game speed altered by virus and antivirus
    /// </summary>
    private float gameSpeed;
    
   

    #endregion

    /// <summary>
    /// Specifiy if a task have window
    /// TASKID: true/false
    /// </summary>
    public List<bool> tasksHaveWindow;

    private void Start()
    {
        virusController = new VirusController(this);
        popups = new List<Popup>();
        gameSpeed = OriginalGameSpeed;

        virusController.forceNewVirus();
        virusController.forceNewVirus();
        virusController.forceNewVirus();
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
                quantity = money;
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
    #endregion
}
