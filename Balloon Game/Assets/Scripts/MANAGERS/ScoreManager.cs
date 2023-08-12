using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int currentScore;
    public Image star;

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
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.UpdateScore();
        star.fillAmount = 0f;
    }

    public void IncreaseScore(int value)
    {

        float totalScore = BalloonManager.Instance._totalPoint;
        currentScore += value;
        if(currentScore <= 0)
        {
            UIManager.Instance.scoreText.color = Color.red;
        }
        
        else if(currentScore == totalScore)
        {
            UIManager.Instance.scoreText.color = Color.green;

        }
        else if (currentScore > 0 && currentScore < totalScore)
        {
            UIManager.Instance.scoreText.color = Color.black;
        }
        star.fillAmount = currentScore /totalScore;
        UIManager.Instance.UpdateScore();
    }
}
