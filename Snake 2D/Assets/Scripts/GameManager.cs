using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText, timeText;
    public GameObject gameOverScreen;
    private int _totalScore;
    private float _time;

    public static GameManager Instance;

    private void Awake()
    {
        MakeSingleton();
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

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
    }


    public void UpdateTime()
    {
        timeText.text =  _time.ToString("F1");
    }

    public void UpdateScore(int score)
    {
        _totalScore += score;
        scoreText.text =  _totalScore.ToString();
    }


    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
