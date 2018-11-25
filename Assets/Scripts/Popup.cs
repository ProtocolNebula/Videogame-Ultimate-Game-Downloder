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

    public Popup()
    {
    }

    /// <summary>
    /// Generate the missing content and positions randomized
    /// </summary>
    /// <returns></returns>
    public Popup randomize()
    {
        // TODO: Calculate positions in function of parent
        // TODO: Apply image positions to avoid overflow image
        if (!imageRef)
        {
            imageRef = sprites[Random.Range(0, sprites.Length)];
        }

        if (posX < 0)
        {
            posX = Random.Range(200, 1590);
        }

        if (posY < 0)
        {
            posY = -Random.Range(255, 600);
        }
        return this;
    }

}
