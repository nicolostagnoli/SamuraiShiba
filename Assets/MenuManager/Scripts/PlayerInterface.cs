using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerInterface : Menu<PlayerInterface>
    {
        private bool _gameIsPaused;
        
        
        private void Update()
        {
            if (Input.GetKeyDown((KeyCode.Escape)))
            {
                if (_gameIsPaused)
                {
                    Resume();
                }
                else
                {
        
                    Pause();
        
                }
            }
        }

        void Resume()
        {
            MenuManager.Instance.SetNonActiveSpecificMenu(PauseMenu.Instance);
            Time.timeScale = 1f;
            _gameIsPaused = false;
        }

        public void Pause()
        {
            
            MenuManager.Instance.SetActiveSpecificMenu(PauseMenu.Instance);
            Time.timeScale = 0f;
            _gameIsPaused = true;
        }

        public static void PlayerDeath()
        {
            
            MenuManager.Instance.SetActiveSpecificMenu(GameOverMenu.Instance);
        }
    }


