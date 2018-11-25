using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlueScreenManager : MonoBehaviour
{
    public Text saldo;
    public Text juegos;
    public Text record;

    // Use this for initialization
    void Start()
    {
        saldo.text = GameManager.money + "";
        juegos.text = GameManager.games + "";

        if (GameManager.money > GameManager.recordMoney)
            GameManager.recordMoney = GameManager.money;
        if (GameManager.games > GameManager.recordGames)
            GameManager.recordGames = GameManager.games;

        record.text = "" + GameManager.recordMoney + "€ de saldo y " + GameManager.recordGames + " archivos descargados";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(0);
            Debug.Log("A key or mouse click has been detected");
        }
    }
}
