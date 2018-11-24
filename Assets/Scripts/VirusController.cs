using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController {

#region "Virus settings"
    // Rate (%) to appearing on each download
    public int minRate = 5;
    public int maxRate = 50;
    public int defaultRateIncrease = 5;
#endregion

#region "Virus system status"
    private int totalVirus;
    // Current appearing rate
    private int currentRate;

    /// <summary>
    /// Antivirus life time left, when is greater than 0, will stop all virus
    /// </summary>
    private float antivirusCooldown = 0;
 #endregion

 #region "Virus controller"
    // Use this for initialization
    public VirusController () {
        totalVirus = 0;
        currentRate = minRate;
	}
	
	// Update is called once per frame
	public void FixedUpdate() {
        if (antivirusCooldownTimer()) { return; }
		
	}

    /// <summary>
    /// Update antivirus cooldown
    /// </summary>
    /// <returns>True if antivirus is running</returns>
    private bool antivirusCooldownTimer()
    {
        if (antivirusCooldown > 0)
        {
            antivirusCooldown -= Time.deltaTime;
            if (antivirusCooldown < 0) antivirusCooldown = 0;
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
    /// Will add a new virus and will increase the counter rate
    /// </summary>
    /// <param name="increaseRate"></param>
    public void forceNewVirus(int addRate = 0)
    {
        if (addRate > 0) increaseRate(addRate);
        totalVirus++;
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
}
