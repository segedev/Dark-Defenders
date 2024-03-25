using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public int currentPlayerIndex;
    public GameObject[] players;
// Start is called before the first frame update
void Awake()
{
    currentPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", 0);
    foreach (GameObject player in players)
    {
        player.SetActive(false);
    }
    players[currentPlayerIndex].SetActive(true);
}
}
