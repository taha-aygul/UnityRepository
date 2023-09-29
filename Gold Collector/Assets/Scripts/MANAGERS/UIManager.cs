using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject startUI, howToPlayUI, playingUI, gameOverUI, gameSuccesUI;
    public float score;
    public TextMeshProUGUI scoreText;

    public static UIManager Instance;
    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void OpenHowToPlay()
    {
        startUI.SetActive(false);
        howToPlayUI.SetActive(true);
    }
    public void CloseHowToPlay()
    {
        startUI.SetActive(true);
        howToPlayUI.SetActive(false);
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        startUI.SetActive(false);
        playingUI.SetActive(true);
    }
     public void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);

    }

    public void GameSucces()
    {
        Time.timeScale = 0;
        gameSuccesUI.SetActive(true);


    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        scoreText.text = score.ToString();

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }








}
