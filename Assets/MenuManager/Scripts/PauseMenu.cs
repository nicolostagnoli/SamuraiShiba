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
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}