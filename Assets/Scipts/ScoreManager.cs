using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textlevel3;
    int score;
    private int scorecounter;
    public Transform wall1;
    
    [HideInInspector]public bool FinishedGame;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int coinValue) //Updates the score
    {
        score += coinValue;
        scorecounter += coinValue;
        text.text = "X" + score.ToString();
        textlevel3.text = "X" + score.ToString();
    }

    void Update()
    {
        if (scorecounter == 5 && wall1 != null) //if the score is equal to 5 and the wall exist it will destory the wall towards the next level
        {
            DestroyWall();
        }
        if (scorecounter == 6)
        {
            score = 0;
            ResetScore();
        }
        if (scorecounter >= 11 )
        {
            FinishedGame = true;
            DestroyWall();
        }
    }

    public void DestroyWall()
    {
        if (scorecounter == 5) //if the score is equal to 5 and the wall exist it will destory the wall towards the next level
        {
            Destroy(wall1.gameObject);
        }
        if (scorecounter >= 11)
        {
           
        }
    }
    public void ResetScore()//will reset the score UI
    {
        text.text = "X" + score.ToString();
        textlevel3.text = "X" + score.ToString();
    }
}
