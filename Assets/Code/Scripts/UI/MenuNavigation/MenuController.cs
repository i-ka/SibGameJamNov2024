using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("Level_0");
    }

public void SettingsPressed()
    {
        SceneManager.LoadScene("SettingsScreen");
    }

public void TitlesPressed()
    {
        SceneManager.LoadScene("Titles");
    }

    public void ExitPressed()

    {
        Application.Quit();
    }

    public void HomePressed()

    {
        SceneManager.LoadScene("StartScreen");
    }
}
