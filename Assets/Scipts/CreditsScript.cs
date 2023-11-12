using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    //[SerializeField] private Animator Credits;
    void Start()
    {
        AudioManager.Instance.Play("CreditsMusic");
        AudioManager.Instance.Stop("MainMenuMusic");
        AudioManager.Instance.Stop("BackgroundMusic");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
