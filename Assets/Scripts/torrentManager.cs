using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torrentManager : MonoBehaviour
{
    public GameObject torrentPanel;

    public Transform torrentContent;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewTorrentPanel(string gameName)
    {
        if (GameManager.instance.numTorrents >= 6)
        {
            GameManager.instance.NoticeMe("Máximo de descargas simultaneas alcanzado");
            return;
        }

        GameObject newGamePanel = Instantiate(torrentPanel, torrentContent);
        newGamePanel.GetComponent<torrentPanelControl>().gameName = gameName;
        GameManager.instance.numTorrents++;
    }

    public void DeleteTorrentPanel(GameObject panel)
    {
        Destroy(panel);
    }

    
}
