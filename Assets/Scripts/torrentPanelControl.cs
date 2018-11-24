using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class torrentPanelControl : MonoBehaviour
{
    private float _currentTime;
    public float totalTime;

    [HideInInspector]
    public string gameName;

    public Text nameText;
    public Text progressText;
    public Image progressBar;
    public Button installButton;

    // Use this for initialization
    void Start()
    {
        nameText.text = gameName;
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime / totalTime;

        progressBar.fillAmount = _currentTime;

        progressText.text = (int)(progressBar.fillAmount * 100) + "%";

        if (progressBar.fillAmount >= 1)
        {
            installButton.interactable = true;
        }
    }

    public void install()
    {
        //algo
    }
}
