using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindowController : MonoBehaviour {

    // ID in manager list
    public int idList;
    public Image me;
    public Image contentImage;
    private Popup settings;

	// Use this for initialization
	void Start () {
    }

    /// <summary>
    /// Apply a set of settings to the popup
    /// </summary>
    /// <param name="settings"></param>
    public void applySettings(Popup settings)
    {
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
        Debug.Log("Closing popup " + idList);
        if (settings.closeable)
        {
            //Destroy(this);
        }
    }
    #endregion
}
