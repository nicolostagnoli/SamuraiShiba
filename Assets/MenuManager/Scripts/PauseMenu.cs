using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu<PauseMenu>
{

    private AudioManager _audioManager;
    public void Start()
    {
            
        _audioManager = FindObjectOfType<AudioManager>();
    }
    public void LowerVolume()
    {
        _audioManager.LowerVolume();
    }
        
    public void AddVolume()
    {
        _audioManager.AddVolume();
    }
    public void Quit()
    {
        Application.Quit();
    }
    
    public void BackToMainMenu()
    {
        MenuManager.Instance.SetNonActiveSpecificMenu(Instance);
        MenuManager.Instance.OpenMenu(MainMenu.Instance);
          
        SceneManager.LoadScene("MenuManager/Scenes/MainMenu");
    }
}