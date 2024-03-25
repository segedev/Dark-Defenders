using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public GameObject startingText;
    public GameObject ScoreContainer;
    public GameObject PauseButton;
    public GameObject CoinHolder;

    public static int numberOfCoins;
    public Text coinsText;
    public Text gameovercoinsText;
    public Text highscoregameovercoinsText;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            PauseButton.SetActive(false);
            ScoreContainer.SetActive(false);
            CoinHolder.SetActive(false);
        }
        coinsText.text = "Coins: " + numberOfCoins;
        gameovercoinsText.text = "COINS: " + numberOfCoins;
        highscoregameovercoinsText.text = "COINS: " + numberOfCoins;
        if(SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
            ScoreContainer.SetActive(true);
            CoinHolder.SetActive(true);
            PauseButton.SetActive(true);
        }
    }
}
