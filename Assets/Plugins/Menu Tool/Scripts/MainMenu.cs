using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Name of the first scene")]
    [Tooltip("The name of the first level must be the exact same")]
    public string firstLevel;

    [Tooltip("The options menu game object")]
    public GameObject optionsScreen;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}

