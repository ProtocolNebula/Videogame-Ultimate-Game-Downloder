using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindowController : MonoBehaviour {

    public GameObject self;
    public Image me;
    public Image contentImage;
    private Popup settings;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
    }

    /// <summary>
    /// Apply a set of settings to the popup
    /// </summary>
    /// <param name="settings">Settings to apply</param>
    /// <param name="gameManager">Game Manager instance to destroy elements</param>
    public void applySettings(Popup settings, GameManager gameManager, GameObject self)
    {
        this.settings = settings;
        this.gameManager = gameManager;
        this.self = self;

        //Sprite sprite = Resources.Load<Sprite>("BotonCerrar");
        // Apply the image
        Sprite sprite = settings.imageRef;
        contentImage.sprite = settings.imageRef;

        // Calculate the new content size
        float newWidth = sprite.textureRect.size.x + contentHorizontalMargin();
        float newHeight = sprite.textureRect.size.y + contentVerticalMargin();
        me.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

        // Set window position
        //me.rectTransform.position = new Vector3(settings.posX, settings.posY);
        me.transform.localPosition = new Vector3(settings.posX, settings.posY);
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
        if (settings.closeable)
        {
            gameManager.DestroyPopup(self);
        }
    }
    #endregion
}
