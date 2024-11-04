using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCallListener : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("InGameMenu");
        }    
    }
}
