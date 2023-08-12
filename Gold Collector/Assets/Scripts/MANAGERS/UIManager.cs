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
        startUI.SetActive(false);
        playingUI.SetActive(true);
    }
     public void GameOver()
    {
        gameOverUI.SetActive(false);
    }

    public void GameSucces()
    {
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
