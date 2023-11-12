using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject playerUI;
    public GameObject playerUI_Level3;
    public GameObject OptionMenu;
    
    [SerializeField] private CharactersSwap whichplayer;

    void Update()
    {
       
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

    public void Resume()
    {
        playerUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        OptionMenu.SetActive(false);
        if (whichplayer.whichCharacter == 2)
        {
            playerUI.SetActive(false);
            playerUI_Level3.SetActive(true);
        }
    }

    public void Pause()
    {
        playerUI_Level3.SetActive(false);
        playerUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        OptionMenu.SetActive(false);
    }

    public void LoadMenu()
    {
        playerUI.SetActive(true);
        Application.Quit();//Cierra el juego en ves de llevarlo al menu principal o los creditos.
        //Debug.Log("Quit");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Options()
    {
        OptionMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
         Application.Quit();
    }
}
