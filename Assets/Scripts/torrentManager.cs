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

    public void NewTorrentPanel(string concat)
    {
        Debug.Log(concat);

        string[] str = concat.Split(" "[0]);

        Debug.Log(str[str.Length - 1]);

        int imageId = int.Parse(str[str.Length - 1]);

        string gameName = "";

        for (int i = 0; i < str.Length-1; i++)
        {
            if(i != str.Length-2)
                gameName += str[i] + " ";
            else
                gameName += str[i] + "";
        }

        if (GameManager.instance.numTorrents >= 6)
        {
            GameManager.instance.NoticeMe("Máximo de descargas simultaneas alcanzado");
            return;
        }

        GameObject newGamePanel = Instantiate(torrentPanel, torrentContent);
        newGamePanel.GetComponent<torrentPanelControl>().gameName = gameName;
        newGamePanel.GetComponent<torrentPanelControl>().imageId = imageId;
        GameManager.instance.numTorrents++;
    }

    public void DeleteTorrentPanel(GameObject panel)
    {
        Destroy(panel);
    }

    
}
