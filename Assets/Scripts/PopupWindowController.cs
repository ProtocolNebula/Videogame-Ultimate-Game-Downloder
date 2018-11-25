using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindowController : MonoBehaviour {

    public GameObject self;
    public Image me;
    public Image contentImage;
    private Popup popupSettings;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
    }

    /// <summary>
    /// Apply a set of popupSettings to the popup
    /// </summary>
    /// <param name="popupSettings">Settings to apply</param>
    /// <param name="gameManager">Game Manager instance to destroy elements</param>
    public void applySettings(Popup popupSettings, GameManager gameManager, GameObject self)
    {
        this.popupSettings = popupSettings;
        this.gameManager = gameManager;
        this.self = self;
        refreshSettings();
    }

    /// <summary>
    /// Will refresh popup with the popupSettings loaded/changed
    /// </summary>
    private void refreshSettings()
    {
        //Sprite sprite = Resources.Load<Sprite>("BotonCerrar");
        // Apply the image
        Sprite sprite = popupSettings.imageRef;
        contentImage.sprite = popupSettings.imageRef;

        // Calculate the new content size
        float newWidth = sprite.textureRect.size.x + contentHorizontalMargin();
        float newHeight = sprite.textureRect.size.y + contentVerticalMargin();
        me.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

        // Set window position
        //me.rectTransform.position = new Vector3(popupSettings.posX, popupSettings.posY);
        me.transform.localPosition = new Vector3(popupSettings.posX, popupSettings.posY);

    }

    /// <summary>
    /// Get the margins top and bottom to calculate the correct height
    /// </summary>
    /// <returns></returns>
    private float contentVerticalMargin ()
    {
        return contentImage.rectTransform.offsetMin.y + -contentImage.rectTransform.offsetMax.y;
    }

    /// <summary>
    /// Get the margins left and right to calculate the correct width
    /// </summary>
    /// <returns></returns>
    private float contentHorizontalMargin()
    {
        return contentImage.rectTransform.offsetMin.x + -contentImage.rectTransform.offsetMax.x;
    }

    // Update is called once per frame
    void Update () {
		
	}

    #region "Buttons controllers"
    public void close()
    {
        Debug.Log("Closing force");
        if (popupSettings.Close())
        {
            gameManager.DestroyPopup(self);
        }
        else
        {
            refreshSettings();
        }
    }
    #endregion
}
