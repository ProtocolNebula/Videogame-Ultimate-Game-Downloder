using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Define the setings for a popup
/// </summary>
public class Popup {

    #region "Window settings"
    public string text;
    public bool closeable = true;

    /// <summary>
    /// If is an "system error" will reproduce different sound
    /// </summary>
    public bool isError = false;

    /// <summary>
    /// If is troll, will appear randomly on try to close (if !closeable)
    /// TODO: Refactor this to another class
    /// </summary>
    public bool isTroll = false;

    /// <summary>
    /// Similar to isTroll, but with other images
    /// TODO: Refactor this to another class
    /// </summary>
    public bool isRansomware = false;
    public float moneyCost = 0;
    public float posX = -1;
    public float posY = -1;
    #endregion

    #region "Content settings"
    public GameObject element;
    public Sprite imageRef;
    #endregion

    /// <summary>
    /// Load all ads images to use when are needed
    /// </summary>
    static private Sprite[] sprites = Resources.LoadAll<Sprite>("Ads");
    static private Sprite[] spritesTroll = Resources.LoadAll<Sprite>("AdsTroll");
    static private Sprite[] spritesRansomware = Resources.LoadAll<Sprite>("AdsRansomeware");

    public Popup()
    {
    }

    /// <summary>
    /// Generate random positions
    /// If no image setted, image will changed too
    /// </summary>
    /// <returns></returns>
    public Popup Randomize()
    {
        if (!imageRef)
        {
            // TODO: Refactor to other class
            if (isRansomware) { imageRef = spritesRansomware[Random.Range(0, spritesRansomware.Length)]; }
            else if (isTroll) { imageRef = spritesTroll[Random.Range(0, spritesTroll.Length)]; }
            else { imageRef = sprites[Random.Range(0, sprites.Length)]; }
        }

        // TODO: Apply image positions to avoid overflow image
        // TODO: Calculate positions in function of parent
        if (!isRansomware)
        {
            posX = Random.Range(200, 1590);
            posY = -Random.Range(255, 600);
        }
        else
        {
            posX = 970;
            posY = -420;
        }
        

        return this;
    }

    /// <summary>
    /// Apply all required actions before close
    /// </summary>
    /// <returns>If return false, popup WILL NOT CLOSED but refreshed</returns>
    public bool Close()
    {
        if (!closeable)
        {
            if (isTroll) { Randomize(); }
        }

        return closeable;
    }

    public bool OkButton()
    {
        if (moneyCost == 0)
        {
            if (isRansomware) { moneyCost = 100; }
            else if (isTroll) { moneyCost = 50; }
        }

        if (isRansomware)
        {
            GameManager.instance.incrementMoney(-moneyCost, true);
            GameManager.instance.NoticeMe("Has pagado " + moneyCost + " € para eliminar el Ransomeware y recuperar tus archivos privados.");
        }
        else if (isTroll)
        {
            GameManager.instance.incrementMoney(-moneyCost, true);
            GameManager.instance.NoticeMe("Has pagado " + moneyCost + " € para eliminar el Troll Virus");
        }

        // Now can be closed
        closeable = true;

        return true;
    }
}
