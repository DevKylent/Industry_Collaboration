using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Programmer: Carlos A. Rodriguez
 * Purpose: This code is meant to hold all the functions executed by the buttons on the Main Menu
 */

public class MainMenuScripts : MonoBehaviour
{

    private void Start()
    {
        AudioManager.Instance.Play("MainMenuMusic");
        AudioManager.Instance.Stop("CreditsMusic");
        AudioManager.Instance.Stop("BackgroundMusic");

    }
    //Variable contains the Options section of the main menu
    [SerializeField] private GameObject optionsMenu = null;

    //Loads Level 1
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");

    }

    //Quits application/game
    public void QuitGame()
    {
        SceneManager.LoadScene("Credits");
        //Application.Quit(); //Will only execute in a build and not on the editor
        //Debug.Log("Quit"); //Debug Log is added to verify the function is working
    }

    //Opens the Options section of the Main menu
    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    //Closes Options section of the main menu
    public void Return()
    {
        optionsMenu.SetActive(false);
    }

}
