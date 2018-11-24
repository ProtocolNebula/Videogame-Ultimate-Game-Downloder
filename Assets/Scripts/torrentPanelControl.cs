﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class torrentPanelControl : MonoBehaviour
{
    private GameObject desktop;
    private float _currentTime;
    public float totalTime;

    [HideInInspector]
    public string gameName;

    public Text nameText;
    public Text progressText;
    public Image progressBar;
    public Button installButton;
    public GameObject iconPrefab;

    // Use this for initialization
    void Start()
    {
        nameText.text = gameName;
        desktop = GameObject.Find("icon_shortcuts");
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
        GameObject newIcon = Instantiate(iconPrefab, desktop.transform);
        newIcon.transform.GetChild(0).GetComponent<Text>().text = gameName;
        Destroy(gameObject);
    }
}
