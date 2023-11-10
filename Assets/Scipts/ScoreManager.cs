using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI text;
    public TextMeshProUGUI textlevel3;

    int score;
    private int i_3DScore;

    public Transform wall1;

    [SerializeField] private GameObject LastCoin;
    [SerializeField] private Image[] UI_Coins;
    
    [HideInInspector]public bool FinishedGame;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore() //Updates the score
    {
        score += 1;
        text.text = "X" + score.ToString();
    }
    public void Change3DScore()
    {
        i_3DScore += 1;
        textlevel3.text = "X" + i_3DScore.ToString();

        for (int i = 0; i < i_3DScore; i++)
        {
            UI_Coins[i].enabled = true;
        }
    }

    void Update()
    {
        if (score == 5 && wall1 != null) //if the score is equal to 5 and the wall exist it will destory the wall towards the next level
        {
            DestroyWall();
        }
        if (score == 5)
        {
            ResetScore();
        }
        if(i_3DScore == 4)
        {
            LastCoin.gameObject.SetActive(true);
        }
        if (i_3DScore >= 5)
        {
            FinishedGame = true;
        }
    }

    public void DestroyWall()
    {
        if (score == 5) //if the score is equal to 5 and the wall exist it will destory the wall towards the next level
        {
            Destroy(wall1.gameObject);
        }
    }
    public void ResetScore()//will reset the score UI
    {
        text.text = "X" + score.ToString();
    }
}
