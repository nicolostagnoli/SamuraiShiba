using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class MainMenu : Menu<MainMenu>
    {

        private bool _gameIsPaused;
        public GameObject item1;
        public GameObject item2;
        public GameObject item3;
        
        

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
            StateNameController.AddHealPotion(3, item1);
            StateNameController.AddStaminaPotion(3,item2);
            StateNameController.AddShuriken(2,item3);
        }
        
        public void OnLevel3Pressed()
        {
         
            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
            SceneManager.LoadScene("Level4/Level4");
            StateNameController.AddHealPotion(5, item1);
            StateNameController.AddStaminaPotion(5,item2);
            StateNameController.AddShuriken(4,item3);
        }
        

        public void OnLevelCranePressed()
        {
         
            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
            SceneManager.LoadScene("Level 1/Scenes/Crane");
            StateNameController.AddHealPotion(2, item1);
            StateNameController.AddStaminaPotion(2,item2);
            StateNameController.AddShuriken(1,item3);
        }
        
        
        public void OnLevelMonkeyPressed()
    
        {
    
            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
            
          
            SceneManager.LoadScene("Level 2/Scenes/Monkey");
            StateNameController.AddHealPotion(4, item1);
            StateNameController.AddStaminaPotion(4,item2);
            StateNameController.AddShuriken(3,item3);
            
            
        }
        
        public void OnLevelWolfPressed()
    
        {
            MenuManager.Instance.CloseMenu();
            MenuManager.Instance.CloseSpecificMenu(Instance);
          
            SceneManager.LoadScene("level 3/Scenes/Wolf");
            StateNameController.AddHealPotion(6, item1);
            StateNameController.AddStaminaPotion(6,item2);
            StateNameController.AddShuriken(6,item3);
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
