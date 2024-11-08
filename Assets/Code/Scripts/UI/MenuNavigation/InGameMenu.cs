using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuUI; // ingame menu
    public GameObject settingsUI; // ingame settings
    public GameObject controlsUI;
    public GameObject titlesUI;
    

    private bool isMenuActive = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuActive)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }
    public void OpenMenu()
    {
        menuUI.SetActive(true); 
        isMenuActive = true;
        
    }
    public void OpenSettings()
    {
        settingsUI.SetActive(true); 
        menuUI.SetActive(false);
        isMenuActive = true;
    }
    public void OpenTitles()
    {
        titlesUI.SetActive(true); 
        menuUI.SetActive(false);
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
        menuUI.SetActive(false);
        settingsUI.SetActive(false);
        titlesUI.SetActive(false);
        controlsUI.SetActive(false);
        isMenuActive = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Выход из игры"); // сообщение в консоли
    }
}
