using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject startingUIPanel, playingUIPanel, endingUIPanel;
    public Image redScreen;
    public TextMeshProUGUI scoreText,timerText,endGameScoreText, endGameScorePercentageText;
    public bool gameStarted , isRed; 
  //  public bool gameEnded ; 
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
    public void UpdateScore()
    {
        scoreText.text = ScoreManager.Instance.currentScore.ToString();
    }
    public void UpdateTime()
    {
        timerText.text = TimerManager.Instance.currentTime.ToString("F1");
    }

    public void StartGame() 
    {
        gameStarted = true;
        startingUIPanel.SetActive(false);
        playingUIPanel.SetActive(true);
    }


    public void EndGame()
    {
        endGameScoreText.text = ScoreManager.Instance.currentScore.ToString();
        endGameScorePercentageText.text = "%"+(ScoreManager.Instance.star.fillAmount * 100).ToString();

        playingUIPanel.SetActive(false);
        endingUIPanel.SetActive(true);
        redScreen.gameObject.SetActive(false);
    }



    public void MakeRedScreenActive()
    {
        redScreen.gameObject.SetActive(true);
        Invoke(nameof(MakeRedScreenInactive), 2f);
    }

    public void MakeRedScreenInactive()
    {
        isRed = false;
        //redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, 1);
        redScreen.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
