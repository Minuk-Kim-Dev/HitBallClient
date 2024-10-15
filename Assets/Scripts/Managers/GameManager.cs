using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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

    GameObject hitCountText;
    public GameObject HitCountText
    {
        get
        {
            if (hitCountText == null)
                hitCountText = GameObject.Find("HitCountText");

            if (hitCountText == null)
                Debug.LogError("HitCountText 오브젝트가 없습니다.");

            return hitCountText;
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

    GameObject turnText;
    public GameObject TurnText
    {
        get
        {
            if (turnText == null)
                turnText = GameObject.Find("TurnText");

            if (turnText == null)
                Debug.LogError("TimerText 오브젝트가 없습니다.");

            return turnText;
        }
    }

    int totalHitCount;
    int TotalRedHitCount 
    {
        get
        {
            return totalHitCount;
        }
        set
        {
            if (totalHitCount == value)
                return;

            totalHitCount = value;
            HitCountText.GetComponent<TMP_Text>().text = $"{totalHitCount}";
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
            ScoreText.GetComponent<TMP_Text>().text = $"{score}";
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

            if (value <= 0)
                value = 0;

            timer = value;
            int ceilingTimer = (int)Math.Ceiling(timer);
            int minutes = (int)(ceilingTimer / 60);
            int seconds = (int)(ceilingTimer % 60);
            TimerText.GetComponent<TMP_Text>().text = $"{minutes}:{seconds}";

            if (timer <= 0 && isFinish == false)
                GameOver();
        }
    }

    public bool isFinish;
    public bool isStart;
    public bool isRolling;
    int hitCombo;

    GameObject gameStartUI;
    public List<GameObject> balls = new List<GameObject>();

    public void Init()
    {
        Score = 0;
        hitCombo = 0;
        TotalRedHitCount = 0;
        Timer = 120;
        isFinish = false;
        isStart = false;

        //TODO : 게임시작 버튼 없으면 게임시작 버튼 생성
        if (gameStartUI == null)
        {
            GameObject ui = Managers.UI.CreateUI("StartUI");
            gameStartUI = ui;
        }

        SetBallsRandom();
    }

    public void SetBallsRandom()
    {
        isRolling = true;
        balls.ForEach(ball => ball.GetComponent<Ball>().SetRandomVelocity());
    }

    public void SetBallsNoneHit()
    {
        balls.ForEach(ball => ball.GetComponent<Ball>().isHit = false);
    }

    /// <summary>
    /// 모든 공이 멈추면 true, 아니면 false
    /// </summary>
    /// <returns></returns>
    public bool CheckAllBallStop()
    {
        int count = 0;

        balls.ForEach(
            ball => { 
                if (ball.GetComponent<Ball>().isMoving == false) 
                    count++; 
            });

        if (count == balls.Count)
        {
            if (!TurnText.activeSelf)
                TurnText.SetActive(true);
            isRolling = false;
            return true;
        }
        else
        {
            if (TurnText.activeSelf)
                TurnText.SetActive(false);
            return false;
        }
    }

    public IEnumerator GameStart()
    {
        Init();
        UnityEngine.Object.Destroy(gameStartUI);

        GameObject readyUI = Managers.UI.CreateUI("ReadyUI");
        
        yield return new WaitUntil(() => CheckAllBallStop());
        yield return new WaitUntil(() => TurnText.activeSelf);

        UnityEngine.Object.Destroy(readyUI);

        isStart = true;
        gameStartUI = null;
    }

    public void EndTurn()
    {
        if (CheckAllBallStop())
        {
            hitCombo = 0;
            SetBallsNoneHit();
        }
    }

    public void HitRedBall()
    {
        Debug.Log("빨간 공을 맞췄습니다!");
        TotalRedHitCount++;
        GetScore();
    }

    public void GetScore()
    {
        Score += 10 * hitCombo++;
    }

    public void HitYellowBall()
    {
        Debug.Log("노란 공을 맞췄습니다..");
        ReduceScore();
    }

    public void ReduceScore()
    {
        Score = Score / 2;
        hitCombo = 0;
    }

    public void GameOver()
    {
        isFinish = true;

        Score newScore = new Score
        {
            TotalScore = Score,
            HitCount = TotalRedHitCount,
            Date = DateTime.Now,
            Id = Managers.Data.Id,
            Major = Managers.Data.Major,
            Name = Managers.Data.Name
        };

        Managers.Instance.StartCoroutine(Managers.Network.CoPostScore(newScore));
    }

    public void Clear()
    {
        scoreText = null;
        timerText = null;
        gameStartUI = null;
        balls.Clear();
    }
}
