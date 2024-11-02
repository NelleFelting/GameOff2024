using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseOptions : MonoBehaviour
{
    public string menuLevel;

    public PauseMenu pauseMenu;
    public GameObject pauseScreen;

    void Start()
    {       

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menuLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void Resume()
    {
        pauseMenu.pauseActive = false;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
