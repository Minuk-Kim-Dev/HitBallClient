using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager
{
    GameObject scoreText;
    public GameObject ScoreText
    {
        get
        {
            if (scoreText == null)
                scoreText = GameObject.Find("ScoreText");

            if (scoreText == null)
                Debug.LogError("ScoreText 오브젝트가 없습니다.");

            return scoreText;
        }
    }

    GameObject timerText;
    public GameObject TimerText
    {
        get
        {
            if (timerText == null)
                timerText = GameObject.Find("TimerText");

            if(timerText == null)
                Debug.LogError("TimerText 오브젝트가 없습니다.");

            return timerText;
        }
    }

    int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (score == value)
                return;

            score = value;
            ScoreText.GetComponent<TMP_Text>().text = $"Score : {score}";
        }
    }

    float timer;
    public float Timer
    {
        get 
        { 
            return timer;
        }
        set 
        {
            if (timer == value)
                return;

            timer = value;
            int ceilingTimer = (int)Math.Ceiling(timer);
            int minutes = (int)(ceilingTimer / 60);
            int seconds = (int)(ceilingTimer % 60);
            TimerText.GetComponent<TMP_Text>().text = $"Timer : {minutes}:{seconds}";
        }
    }

    int hitCombo;

    public void Init()
    {
        Score = 0;
        hitCombo = 0;
        Timer = 60;
    }

    public void HitRedBall()
    {
        Debug.Log("빨간 공을 맞췄습니다!");
        hitCombo++;
        GetScore();
    }

    public void GetScore()
    {
        Score += hitCombo;
    }

    public void HitYellowBall()

    {
        Debug.Log("노란 공을 맞췄습니다..");
        ReduceScore();
    }

    public void ReduceScore()
    {
        if(hitCombo == 0)
        {
            Score--;
        }
        else
        {
            Score -= hitCombo;
        }

        hitCombo = 0;
    }

    public void GameOver()
    {

    }
}
