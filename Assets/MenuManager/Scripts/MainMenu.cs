using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class MainMenu : Menu<MainMenu>
    {

        private bool _gameIsPaused;
       

        

        public void OnPlayPressed()
        {
            if (MenuManager.Instance != null)
            //MenuManager.Instance.OpenMenu(PlayerInterface.Instance);
            MenuManager.Instance.CloseMenu();
            SceneManager.LoadScene("Tutorial/Scenes/Tutorial");
        }

        public void OnSettingsPressed()
        {
            if (MenuManager.Instance != null && SettingsMenu.Instance != null)
            {
                MenuManager.Instance.OpenMenu(SettingsMenu.Instance);
            }
        }

        public void onCreditsPressed()
        {
            if (MenuManager.Instance != null && CreditScreen.Instance != null)
            {
                MenuManager.Instance.OpenMenu(CreditScreen.Instance);
            }
        }

       
        public void Quit()
        {
            Application.Quit();
        }
        
        
        public void ReloadCurrentScene()
        {
            if (GameOverMenu.Instance != null)
            MenuManager.Instance.SetNonActiveSpecificMenu(GameOverMenu.Instance);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
        
        public void LoadMainMenu()
    
        {
            if (GameOverMenu.Instance != null)
            MenuManager.Instance.SetNonActiveSpecificMenu(GameOverMenu.Instance);
            MenuManager.Instance.OpenSpecificMenu(MainMenu.Instance);
            SceneManager.LoadScene("MenuManager/Scenes/MainMenu");
            
            
        }
        

    }
