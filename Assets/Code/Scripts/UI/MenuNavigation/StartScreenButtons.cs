using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreenButtons : MonoBehaviour
{
    public GameObject settingsUI;
    public GameObject titlesUI;
    public GameObject mainMenuUI;
    public GameObject controlsUI;
    private bool isMenuActive = false;

public void PlayPressed()
    {
        SceneManager.LoadScene("Level_0");
    }

public void OpenSettings()
    {
        settingsUI.SetActive(true); 
        mainMenuUI.SetActive(false);
        isMenuActive = true;
    }
public void OpenTitles()
    {
        titlesUI.SetActive(true); 
        mainMenuUI.SetActive(false);
        isMenuActive = true;
    }
public void OpenControls()
    {
        controlsUI.SetActive(true); 
        settingsUI.SetActive(false);
        isMenuActive = true;
    }
public void CloseMenu()
    {
        mainMenuUI.SetActive(true);
        settingsUI.SetActive(false);
        titlesUI.SetActive(false);
        controlsUI.SetActive(false);
        isMenuActive = false;
    }

public void ExitPressed()

    {
        Application.Quit();
    }



// public void SettingsWindowPressed()
//     {
//         SceneManager.LoadScene("SettingsWindow");

//     }
}
