using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class internetManager : MonoBehaviour
{
    public GameObject gamePanel;

    public Transform gameContent;

    public List<string> gameNames;
    public List<string> developerNames;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            NewGamePanel();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TabButtons(GameObject tab)
    {
        tab.transform.SetSiblingIndex(1);
    }

    public void NewGamePanel()
    {
        GameObject newGamePanel = Instantiate(gamePanel, gameContent);
        newGamePanel.GetComponent<gamePanelControl>().gameName = gameNames[Random.Range(0, gameNames.Count - 1)];
        newGamePanel.GetComponent<gamePanelControl>().developer = developerNames[Random.Range(0, developerNames.Count - 1)];
        newGamePanel.GetComponent<gamePanelControl>().price = Random.Range(0, 50);
    }

    public void DeleteGamePanel(GameObject panel)
    {
        Destroy(panel);
    }

    public void ReloadPanel(GameObject panel)
    {
        if (GameManager.instance.numTorrents >= 6)
            return;
            
        GameManager.instance.NoticeMe("Se ha añadido un juego a torrentGames");
        GameManager.instance.virusController.newVirus();

        panel.GetComponent<Animator>().SetTrigger("del");
        StartCoroutine(WaitThenDoThings(panel));
    }

    IEnumerator WaitThenDoThings(GameObject panel)
    {
        yield return new WaitForSeconds(0.2f);

        // Now do some stuff...
        NewGamePanel();
        Destroy(panel);
    }
}
