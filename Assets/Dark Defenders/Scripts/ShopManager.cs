using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int currentPlayerIndex;
    public GameObject[] playerModels;

    public PlayerBlueprint[]  players;
    public Button buyButton;
    // Start is called before the first frame update
    void Start()
    {
        foreach (PlayerBlueprint player in players)
        {
            if(player.price == 0)
            {
                player.isUnlocked = true;
            }
            else
            {
                player.isUnlocked = PlayerPrefs.GetInt(player.name, 0)== 0 ? false: true;
            }
        }

        currentPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);
        foreach (GameObject player in playerModels)
        {
            player.SetActive(false);
        }
        playerModels[currentPlayerIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void ChangeNext()
    {
        playerModels[currentPlayerIndex].SetActive(false);
        currentPlayerIndex++;

        if(currentPlayerIndex == playerModels.Length)
        {
            currentPlayerIndex = 0;
        }

        playerModels[currentPlayerIndex].SetActive(true);

        PlayerBlueprint p = players[currentPlayerIndex];
        if(!p.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
    }

    public void ChangePrevious()
    {
        playerModels[currentPlayerIndex].SetActive(false);
        currentPlayerIndex--;
        if(currentPlayerIndex == -1)
        {
            currentPlayerIndex = playerModels.Length - 1;
        }
        playerModels[currentPlayerIndex].SetActive(true);

        PlayerBlueprint p = players[currentPlayerIndex];
        if(!p.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
    }

    public void UnlockCar()
    {
        PlayerBlueprint p = players[currentPlayerIndex];
        PlayerPrefs.SetInt(p.name, 1);
        PlayerPrefs.SetInt("SelectedPlayer", currentPlayerIndex);
        p.isUnlocked = true;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - p.price);
    }

    private void UpdateUI()
    {
        PlayerBlueprint p = players[currentPlayerIndex];
        if(p.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<Text>().text =  "$" + p.price;
            if(p.price < PlayerPrefs.GetInt("Coins", 0))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }
}
