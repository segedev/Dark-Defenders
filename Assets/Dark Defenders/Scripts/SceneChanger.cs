using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //Load MainMenu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    //Load SelectPlayer
    public void SelectPlayer()
    {
        SceneManager.LoadScene(1);
    }
    //Load PlayScene
    public void PlayScene()
    {
        SceneManager.LoadScene(2);
    }
    //Load CreditScene
    public void CreditScene()
    {
        SceneManager.LoadScene(3);
    }
    //Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
