using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0.0f;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;
    public Text scoreText;
    public Text gameoverScoreText;
    public Text newScoreText;
    public GameObject newScoreTextHolder;
    public GameObject ScoreTextHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(score >= scoreToNextLevel)
        {
            LevelUp();
        }
        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
        newScoreText.text = ((int)score).ToString();
        gameoverScoreText.text = ((int)score).ToString();
        if(PlayerManager.gameOver == true)
        {
            if(score > PlayerPrefs.GetInt("HighScore", 0))
            {
                newScoreTextHolder.SetActive(true);
            }
            else
            {
                ScoreTextHolder.SetActive(true);
            }
        }
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int)score); 
        }
        
    }

    void LevelUp()
    {
        if(difficultyLevel == maxDifficultyLevel)
        {
            return;
        }
        scoreToNextLevel *= 2;
        difficultyLevel++;

        GetComponent<PlayerController>().SetSpeed(difficultyLevel);
        Debug.Log(difficultyLevel);
    }
}
