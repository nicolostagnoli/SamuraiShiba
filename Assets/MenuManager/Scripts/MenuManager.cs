using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MenuManager : Singleton<MenuManager>
{

    public MainMenu mainMenuPrefab;

    public SettingsMenu settingsMenuPrefab;

    public CreditScreen creditsMenuPrefab;
    
    public PlayerInterface playerInterfacePrefab;

    private Stack<Menu> _menusStack = new Stack<Menu>();

    

    private Transform _menuParent;

    private void InizializeMenu()
    {
        Menu[] menus = new Menu[] {mainMenuPrefab, settingsMenuPrefab, creditsMenuPrefab, playerInterfacePrefab};

        if (_menuParent == null)
        {
            GameObject parentMenu = new GameObject("Menu");
            parentMenu.transform.position = Vector3.zero;
            _menuParent = parentMenu.transform;
            DontDestroyOnLoad(_menuParent.gameObject);
        }
        
        
        
        foreach (Menu menu in menus)
        {
            Menu menuInstance = Instantiate(menu, _menuParent);
            menuInstance.gameObject.transform.SetParent(_menuParent.transform);

            if (menu != mainMenuPrefab)
            {
                menuInstance.gameObject.SetActive(false);
                Debug.Log("diverso da main menu, disattivo");
            }
            else
            {
                Debug.Log("uguale a main menu, attivo");
                OpenSpecificMenu(MainMenu.Instance);
            }
        }
    }

    private void Start()
    {
        InizializeMenu();
    }

    public void OpenMenu(Menu menuInstance)
    {
        if (menuInstance != null)
        {
            foreach (Menu menu in _menusStack)
            {
                menu.gameObject.SetActive(false);
            }
            menuInstance.gameObject.SetActive(true);
            _menusStack.Push(menuInstance);
        }
    }
    
    public void OpenSpecificMenu(Menu menuInstance)
    {
        if (menuInstance != null)
        {
            foreach (Menu menu in _menusStack)
            {
                menu.gameObject.SetActive(false);
            }
            menuInstance.gameObject.SetActive(true);
            _menusStack.Push(menuInstance);
        }
    }
    
    public void CloseSpecificMenu(Menu menuInstance)
    {
        if (menuInstance != null)
        {
            foreach (Menu menu in _menusStack)
            {
                menu.gameObject.SetActive(false);
            }
            menuInstance.gameObject.SetActive(false);
            _menusStack.Push(menuInstance);
        }
    }


    public void CloseMenu()
    {
        if (_menusStack.Count > 1)
        {
            Menu topMenu = _menusStack.Pop();
            topMenu.gameObject.SetActive(false);

            Menu currentMenu = _menusStack.Peek();
            currentMenu.gameObject.SetActive(true); 
        }
        else if (_menusStack.Count > 0)
        {  Menu topMenu = _menusStack.Peek(); 
            topMenu.gameObject.SetActive(false);
           
            
        }
        
    }
    public void LoadLevel1()
    
    {
        SceneManager.LoadScene("Level 1/Scenes/Level1");
    }
    
    public void LoadTutorial()
    
    {
        SceneManager.LoadScene("Tutorial/Scenes/Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
