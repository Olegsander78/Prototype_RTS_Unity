using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    public GameObject StartMenuWindow;
    public GameObject LevelMenuWindow;
    public GameObject WinMenuWindow;
    public GameObject GameOverMenuWindow;

    private bool _isActiveStartMenuWindow;
    private bool _isActiveWinMenuWindow = false;
    private bool _isActiveGameOverMenuWindow = false;


    private void Start()
    {
       OpenStartMenuWindow();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isActiveStartMenuWindow && (!_isActiveWinMenuWindow || !_isActiveGameOverMenuWindow))
        {           
            OpenStartMenuWindow();
        }else if (Input.GetKeyDown(KeyCode.Escape) && _isActiveStartMenuWindow && (!_isActiveWinMenuWindow || !_isActiveGameOverMenuWindow))
        {
            CloseStartMenuWindow();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (_isActiveWinMenuWindow || _isActiveGameOverMenuWindow))
        {
            return;
        }
    }

    public void OpenStartMenuWindow()
    {
        LevelMenuWindow.SetActive(false);
        StartMenuWindow.SetActive(true);
        WinMenuWindow.SetActive(false);
        GameOverMenuWindow.SetActive(false);
        _isActiveStartMenuWindow = true;
        Time.timeScale = 0f;
    }

    public void CloseStartMenuWindow()
    {
        LevelMenuWindow.SetActive(true);
        StartMenuWindow.SetActive(false);
        WinMenuWindow.SetActive(false);
        GameOverMenuWindow.SetActive(false);
        _isActiveStartMenuWindow = false;
        Time.timeScale = 1f;
    }

    public void StartRestartScene()
    {
        
        CloseStartMenuWindow();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartScene()
    {

        CloseStartMenuWindow();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenWinMenuWindow()
    {
        WinMenuWindow.SetActive(true);
        _isActiveWinMenuWindow = true;
    }
    public void CloseWinMenuWindow()
    {
        WinMenuWindow.SetActive(false);
        _isActiveWinMenuWindow = false;
    }

    public void OpenGameOverMenuWindow()
    {
        GameOverMenuWindow.SetActive(true);
        _isActiveGameOverMenuWindow = true;
    }

    public void CloseGameOverMenuWindow()
    {
        GameOverMenuWindow.SetActive(false);
        _isActiveGameOverMenuWindow = false;
    }
}
