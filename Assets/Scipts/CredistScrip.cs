using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CredistScrip : MonoBehaviour
{
    public void Start()
    {
        AudioManager.Instance.Play("CreditsMusic");
        AudioManager.Instance.Stop("MainMenuMusic");
        AudioManager.Instance.Stop("BackgroundMusic");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //AudioManager.Instance.Stop("CreditsMusic");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level1");
        //AudioManager.Instance.Stop("CreditsMusic");
    }
}
