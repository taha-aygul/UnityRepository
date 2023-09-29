using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeAndScoreManager : MonoBehaviour
{

    [SerializeField] private int starScoreMultiplier;
    public static TimeAndScoreManager Instance;
    private float time = 0 ;
    private int scoreTime = 0, starCount =0, scoreTotal =0, highScore;
    private bool newHighScore;
    private GameManager _gameManager;

    public float Time { get => time; private set => time = value; }
    public int ScoreTotal { get => scoreTotal; private set => scoreTotal = value; }
    public int ScoreTime { get => scoreTime; private set => scoreTime = value; }
    public int StarCount { get => starCount; private set => starCount = value; }
    public int HighScore { get => highScore; set => highScore = value; }
    public bool NewHighScore { get => newHighScore; set => newHighScore = value; }

    private void Awake()
    {
        MakeSingleton();
    }
    private void Start()
    {
        _gameManager = GameManager.Instance;
        highScore = PlayerPrefs.GetInt("highscore", highScore);
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }


    public void UpdateTime()
    {
        if (!_gameManager.IsGameOver() || !_gameManager.IsGamePaused())
        {
            time += UnityEngine.Time.deltaTime;
        }
    }

    public void UpdateScore()
    {
        scoreTotal = Mathf.RoundToInt(time) + starCount* starScoreMultiplier;
        if (scoreTotal>highScore)
        {
            highScore = scoreTotal;
            newHighScore = true;
        }

    }

    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void IncreaseStarCount()
    {
        starCount += 1;

    }
}
