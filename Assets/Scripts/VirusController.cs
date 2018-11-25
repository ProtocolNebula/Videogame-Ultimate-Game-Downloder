using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController {

    #region "Virus settings"
    // Rate (%) to appearing on each download
    public int minRate = 5;
    public int maxRate = 50;
    public int defaultRateIncrease = 5;

    /// <summary>
    /// Minimum game speed (x0.) to decrease speed
    /// </summary>
    public float minGameSpeed = 0.4f; // Minimum game speed: 40%

    /// <summary>
    /// Quantity of speed decreasing for each lag virus
    /// </summary>
    public float virusLagDecrease = 0.02f; // Each virus decrease 0.02%

    #endregion

    #region "Virus system status"
    /// <summary>
    /// Count with the total of virus that are installed
    /// </summary>
    private int totalVirus;

    /// <summary>
    /// Total virus that cause lag
    /// </summary>
    private int totalVirusLag;

    // Current appearing rate
    private int currentRate;

    /// <summary>
    /// Antivirus life time left, when is greater than 0, will stop all virus
    /// </summary>
    private float antivirusCooldown = 0;

    /// <summary>
    /// Reference to game manager to interact if virus or other
    /// </summary>
    private GameManager gameManager;
    #endregion

    #region "Virus controller
    // Use this for initialization
    public VirusController(GameManager gameManager) {
        totalVirus = 0;
        totalVirusLag = 0;
        currentRate = minRate;
        this.gameManager = gameManager;
    }

    /// <summary>
    /// TODO: If not necessary, remove (and from GameManager)
    /// </summary>
    public void FixedUpdate() {
        if (antivirusCooldownTimer()) { return; }

    }

    /// <summary>
    /// Update antivirus cooldown
    /// TODO: Move to coroutine
    /// </summary>
    /// <returns>True if antivirus is running</returns>
    private bool antivirusCooldownTimer()
    {
        if (antivirusCooldown > 0)
        {
            antivirusCooldown -= Time.deltaTime;
            if (antivirusCooldown <= 0)
            {
                antivirusCooldown = 0;
                updateGameSpeed();
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// Increase the antivirus cooldown
    /// </summary>
    /// <param name="time"></param>
    public void addAntivirus(float time)
    {
        antivirusCooldown += time;
    }

    /// <summary>
    /// Will try to add a virus and will increase the current rate with the defaultRateIncrease
    /// </summary>
    /// <returns>True if virus is added</returns>
    public bool newVirus()
    {
        int random = Random.Range(0, 100);
        increaseRate(defaultRateIncrease);

        if (random <= currentRate)
        {
            forceNewVirus(0);
            return true;
        }

        return true;
    }

    /// <summary>
    /// Generate a new random virus and will increase the counter rate
    /// </summary>
    /// <param name="increaseRate"></param>
    public void forceNewVirus(int addRate = 0)
    {
        if (addRate > 0) increaseRate(addRate);
        generateRandomVirus();
    }

    /// <summary>
    /// This will increase the virus rate if can be incremented
    /// </summary>
    /// <param name="increaseRate"></param>
    public void increaseRate(int increaseRate)
    {
        if (currentRate >= maxRate) return;

        // Increase rate limited by max rate
        currentRate += increaseRate;
        if (currentRate > maxRate) { currentRate = maxRate; }
    }
    #endregion

    #region "Virus generation and speed control"

    /// <summary>
    /// Calculate and modify the game speed
    /// </summary>
    private void updateGameSpeed()
    {
        if (antivirusCooldown > 0)
        {
            // Anti virus working
            gameManager.GameSpeed = GameManager.OriginalGameSpeed;
        }
        else
        {
            // No anti virus working
            float gameSpeed = GameManager.OriginalGameSpeed - (totalVirusLag * virusLagDecrease);

            // Minimum game speed
            if (gameSpeed < minGameSpeed) { gameSpeed = minGameSpeed; }

            gameManager.GameSpeed = gameSpeed;
        }
        Debug.Log("Updated game speed to: " + gameManager.GameSpeed);
    }

    /// <summary>
    /// Generate a new random virus type and increment the total virus
    /// TODO: Refactor (create a new class for each virus and store in a list with available virus)
    /// </summary>
    private void generateRandomVirus()
    {
        totalVirus++;
        int virus = Random.Range(0, 5) + 1;

        switch (virus)
        {
            case 1: // Lag virus
                this.addVirusTypeLag();
                break;

            case 2: // Paypal hack
                this.addVirusTypePaypalHack();
                break;

            case 3: // Multi pop-up
                this.addVirusPopup();
                break;

            case 4: // Pop-up troll
                this.addVirusPopupTroll();
                break;

            case 5: // ransomware
                this.addVirusRansomeware();
                break;
        }

        updateGameSpeed();
    }
    #endregion

    #region "Virus types addition (refactor pending)"
    private void addVirusTypeLag()
    {
        Debug.Log("Adding virus lag");
        totalVirusLag++;
    }


    private void addVirusTypePaypalHack()
    {
        Debug.Log("Adding paypal hack virus");
        float moneyRemoved = gameManager.incrementMoney(-50, true);
        if (moneyRemoved > 0)
        {

        }
    }

    private void addVirusPopup() {
        Debug.Log("Adding multi popup virus");
        Popup popup = new Popup().randomize();

        gameManager.NewPopup(popup);
    }

    private void addVirusPopupTroll() {
        Debug.Log("Adding popup troll virus");
    }

    private void addVirusRansomeware() {
        Debug.Log("Adding ransomeware");
    }
    #endregion
}
