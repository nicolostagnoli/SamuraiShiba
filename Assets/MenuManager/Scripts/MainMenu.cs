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
            MenuManager.Instance.CloseSpecificMenu(Instance);
            SceneManager.LoadScene("Tutorial/Scenes/Tutorial");
        }

        public void OnSettingsPressed()
        {
            if (MenuManager.Instance != null && SettingsMenu.Instance != null)
            {
                MenuManager.Instance.OpenMenu(SettingsMenu.Instance);
            }
        }
        
        public void OnLevelSelectPressed()
        {
            if (MenuManager.Instance != null && SelectLevel.Instance != null)
            {
                MenuManager.Instance.OpenMenu(SelectLevel.Instance);
            }
        }
        
        public void OnDifficultySelectPressed()
        {
            if (MenuManager.Instance != null && DifficultySelect.Instance != null)
            {
                MenuManager.Instance.OpenMenu(DifficultySelect.Instance);
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
            MenuManager.Instance.OpenMenu(MainMenu.Instance);
          
            SceneManager.LoadScene("MenuManager/Scenes/MainMenu");
            
            
        }
        
        public void OnLevel1Pressed()
        {
            if (MenuManager.Instance != null)
            //MenuManager.Instance.OpenMenu(PlayerInterface.Instance);
            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
            SceneManager.LoadScene("Level 1/Scenes/level1");
        }

        public void OnLevel2Pressed()
        {
       

            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
            SceneManager.LoadScene("Level 2/Scenes/level2");
        }
        
        public void OnLevel3Pressed()
        {
         
            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
            SceneManager.LoadScene("Level4/Level4");
        }
        

        public void OnLevelCranePressed()
        {
         
            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
            SceneManager.LoadScene("Level 1/Scenes/Crane");
        }
        
        public void OnLevelMonkeyPressed()
    
        {
    
            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
            
          
            SceneManager.LoadScene("Level 2/Scenes/Monkey");
            
            
        }
        
        public void OnLevelWolfPressed()
    
        {
            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
          
            SceneManager.LoadScene("level 3/Scenes/Wolf");
        }

        public void setDifficultyEasy()
        {
            StateNameController.SetEasyDifficulty();
            MenuManager.Instance.SetNonActiveSpecificMenu(Instance);
            MenuManager.Instance.OpenMenu(MainMenu.Instance);
          
            SceneManager.LoadScene("MenuManager/Scenes/MainMenu");
        }

        public void setDifficultyNormal()
        {
            StateNameController.SetMediumDifficulty();
            MenuManager.Instance.SetNonActiveSpecificMenu(Instance);
            MenuManager.Instance.OpenMenu(MainMenu.Instance);
          
            SceneManager.LoadScene("MenuManager/Scenes/MainMenu");
        }

        public void setDifficultyHard()
        {
            StateNameController.SetHardDifficulty();
            MenuManager.Instance.SetNonActiveSpecificMenu(Instance);
            MenuManager.Instance.OpenMenu(MainMenu.Instance);
          
            SceneManager.LoadScene("MenuManager/Scenes/MainMenu");
        }
        
        

    }
