using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamePanelControl : MonoBehaviour {

    //valores que le pasas desde el manager
    [HideInInspector]
    public string gameName;
    [HideInInspector]
    public string developer;
    [HideInInspector]
    public int price;
    [HideInInspector]
    public Image image;

    public Text nameText;
    public Text developerText;
    public Text priceText;
    public Image imagePanel;
    public Button downloadButton;
    

	// Use this for initialization
	void Start () {
        nameText.text = gameName;
        developerText.text = developer;
        priceText.text = price + "$";
        nameText.text = gameName;
        imagePanel = image;

        torrentManager tM = GameObject.Find("torrent_window").GetComponent<torrentManager>();
        downloadButton.onClick.AddListener(delegate { tM.NewTorrentPanel(gameName); });
        
        internetManager iM = GameObject.Find("Internet_window").GetComponent<internetManager>();

        //downloadButton.onClick.AddListener(iM.NewGamePanel);
        //downloadButton.onClick.AddListener(delegate { iM.DeleteGamePanel(gameObject); });
        downloadButton.onClick.AddListener(delegate { iM.ReloadPanel(gameObject); });
    }

    public void FakeDownloadButton()
    {
        Popup popup = new Popup().Randomize();
        GameManager.instance.NewPopup(popup);
    }
}
