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

public void SettingsWindowPressed()
    {
        SceneManager.LoadScene("SettingsWindow");

    }

public void ControllsWindowPressed()
    {
        SceneManager.LoadScene("ControlWindow");

    }

public void ControllsPressed()
    {
        SceneManager.LoadScene("ControlScreen");

    }

public void TitlesPressed()
    {
        SceneManager.LoadScene("TitlesScreen");
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
