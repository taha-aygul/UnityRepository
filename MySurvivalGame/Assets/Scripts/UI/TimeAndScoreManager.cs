using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeAndScoreManager : MonoBehaviour
{

    // TODO yýldýz 
    [SerializeField] private float remainingTime;
    private float firstRemainingTime;
    public static TimeAndScoreManager Instance;
    private int scoreTime = 0, scoreZombie, scoreTotal = 0, highScore;
    private bool newHighScore;
    private GameManager _gameManager;

    public float Time { get => remainingTime; private set => remainingTime = value; }
    public int ScoreTotal { get => scoreTotal; private set => scoreTotal = value; }
    public int ScoreTime { get => scoreTime; private set => scoreTime = value; }
    public int HighScore { get => highScore; set => highScore = value; }
    public bool NewHighScore { get => newHighScore; set => newHighScore = value; }
    public float FirstRemainingTime { get => firstRemainingTime; set => firstRemainingTime = value; }

    private void Awake()
    {
        MakeSingleton();
        FirstRemainingTime = remainingTime;
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
            remainingTime -= UnityEngine.Time.deltaTime;
            if (remainingTime <= 0.2f)
            {
                scoreTime = (int)firstRemainingTime;
                remainingTime = 0f;
                GameManager.Instance.GameOver();
            }
        }
    }

    public void UpdateScore()
    {
        scoreTotal = scoreZombie + scoreTime;
        if (scoreTotal > highScore)
        {
            highScore = scoreTotal;
            newHighScore = true;
        }

    }


    public void AddScore(int amount)
    {

        scoreZombie += amount;
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




}
