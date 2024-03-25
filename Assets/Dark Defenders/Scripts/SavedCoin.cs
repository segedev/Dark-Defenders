using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedCoin : MonoBehaviour
{
    public Text Coins_Text;
    public Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
        Coins_Text.text = "Coins: " + PlayerPrefs.GetInt("Coins", 0).ToString();
    }
}
