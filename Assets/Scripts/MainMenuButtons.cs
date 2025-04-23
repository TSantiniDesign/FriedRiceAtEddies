/*****************************************************************************
// File Name : MainMenuButtons.cs
// Author : Thomas Santini
// Creation Date : March 25th, 2025 
//
// Brief Description : This script is for the main menu buttons, creating 
functions that, when called, will load the various levels or quit the game.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    /// <summary>
    /// Loads the second level.
    /// </summary>
    public void StartScene()
    {
        SceneManager.LoadSceneAsync("LevelThree");
    }

    /// <summary>
    /// Loads the first level.
    /// </summary>
    public void LevelOne()
    {
        SceneManager.LoadSceneAsync("LevelOne");
    }

    /// <summary>
    /// Loads the second level.
    /// </summary>
    public void LevelTwo()
    {
        SceneManager.LoadSceneAsync("LevelTwo");
    }

    /// <summary>
    /// Loads the Main Menu.
    /// </summary>
    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    /// <summary>
    /// Quits the game and sets isPlaying to false.
    /// </summary>
    public void QuitScene()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
