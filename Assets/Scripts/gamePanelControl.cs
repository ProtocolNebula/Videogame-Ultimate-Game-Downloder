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
    public Sprite image;
    [HideInInspector]
    public int imageId;

    public Text nameText;
    public Text developerText;
    public Text priceText;
    public Image imagePanel;
    public List<Button> downloadButtons;
    

	// Use this for initialization
	void Start () {
        nameText.text = gameName;
        developerText.text = developer;
        priceText.text = price + " €";
        nameText.text = gameName;
        imagePanel.sprite = image;

        int randomNumber = Random.Range(0, 3);

        for (int i = 0; i < downloadButtons.Count; i++)
        {
            if(i == randomNumber)
            {
                string concat = gameName + " " + imageId;

                torrentManager tM = GameObject.Find("torrent_window").GetComponent<torrentManager>();
                downloadButtons[i].onClick.AddListener(delegate { tM.NewTorrentPanel(concat); });

                internetManager iM = GameObject.Find("Internet_window").GetComponent<internetManager>();
                downloadButtons[i].onClick.AddListener(delegate { iM.ReloadPanel(gameObject); });

                //downloadButton.onClick.AddListener(iM.NewGamePanel);
                //downloadButton.onClick.AddListener(delegate { iM.DeleteGamePanel(gameObject); });
            }
            else
            {
                downloadButtons[i].onClick.AddListener(FakeDownloadButton);
            }
        }
        

    }

    public void FakeDownloadButton()
    {
        Popup popup = new Popup().Randomize();
        GameManager.instance.NewPopup(popup);
    }
}
