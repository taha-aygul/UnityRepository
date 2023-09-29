using TMPro;
using UnityEngine;

public class TimeAndScoreManager : MonoBehaviour
{

    // TODO y�ld�z 
    public static TimeAndScoreManager Instance;
    private float time = 0 ;
    private int scoreTime = 0, coinCount =0, scoreTotal =0, highScore;
    private bool newHighScore;
    private GameManager _gameManager;

    public float Time { get => time; private set => time = value; }
    public int ScoreTotal { get => scoreTotal; private set => scoreTotal = value; }
    public int ScoreTime { get => scoreTime; private set => scoreTime = value; }
    public int CoinCount { get => coinCount; private set => coinCount = value; }
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
        scoreTotal = Mathf.RoundToInt(time) + coinCount;
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


    public void Gain()
    {
        coinCount += 1;

    }

    public void Spend(int price)
    {
        if (coinCount >= price)
        {
            coinCount -= price;

            // TODO : sat�n al�nan �zelli�i karaktere y�kle
        }
    }
}
