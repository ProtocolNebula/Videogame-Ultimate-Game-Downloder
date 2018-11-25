using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTroll : Popup {
    #region "Window settings"
    public bool closeable = false;
    #endregion

    public new bool Close()
    {
        Debug.Log("Randomizing?");
        if (closeable) { return true; }
        Debug.Log("Randomizing");
        // Generate a new position
        Randomize();
        Debug.Log("Randomized");
        return false;
    }
}
