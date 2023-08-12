using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int totalScore;
    [SerializeField] private int scoreIncreaseAmount;

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


    public void IncreaseScore()
    {
        totalScore += scoreIncreaseAmount;
        UIManager.Instance.WriteScore();
    }

    public int GetTotalScore()
    {
        return totalScore;
    }




}
