using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public string levelSelect;
    public string mainMenu;
    public bool isPaused;
    public GameObject pauseMenuCanvas;
	
	// Update is called once per frame
	void Update ()
    {
	    if(isPaused)
        {
            //turn pause menu on when activate the pause button
            pauseMenuCanvas.SetActive(true);
            //pause game
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            //resume game
            Time.timeScale = 1f;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //unpaused to pause and vise versa
            // isPaused = !isPaused;
            PauseUnpause();
        }
	}
    public void PauseUnpause()
    {
        isPaused = !isPaused;
    }
    public void Resume()
    {
        isPaused = false;
    }
    public void LevelSelect()
    {
        Application.LoadLevel(levelSelect);
    }
    public void QuitGame()
    {
        Application.LoadLevel(mainMenu);
    }
}
