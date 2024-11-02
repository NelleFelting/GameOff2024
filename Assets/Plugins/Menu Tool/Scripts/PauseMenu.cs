using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseScreen;
    public AudioSource[] audioIgnoringPause;
    public bool pauseActive;


    private void Start()
    {
        //Allows specified sounds to continue in pause screen
        for (int i = 0; i < audioIgnoringPause.Length; i++)
        {
            audioIgnoringPause[i].ignoreListenerPause = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && pauseActive == false)
        {
            pauseActive = true;
            pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseActive == true)
        {
            pauseActive = false;
            pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
}
