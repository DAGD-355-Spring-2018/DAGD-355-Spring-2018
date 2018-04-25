using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    //For pitch of sound to change during pause, try something like this inside the audio script:
    /*
     if (PauseMenu.GameIsPaused)
     {
        VARIABLE.source.pitch *= 0.5f;
     }
     */

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
   
    // Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    } 

    public void LoadMenu ()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading menu...");
        //SceneManager.LoadScene("Menu");
        //Probably want to use a variable for this instead.
    }

    public void QuitGame ()
    {
        Debug.Log("Quitting game...");  //Leave in while using editor.
        Application.Quit();  //Will not do anything while in editor; only when built.
    }
}
