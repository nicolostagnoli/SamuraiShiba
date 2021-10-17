using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1/Scenes/Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
