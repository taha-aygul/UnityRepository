using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startUI, playUI, failUI, succesUI;
    public bool isGameFailed, isGameSuccesful, isGameStarted, firstThrow = true;
    public TextMeshProUGUI scoreText, levelFailedScoreText, stageText;


    public static UIManager Instance;

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

    public void PlayButton()
    {
        StartCoroutine(nameof(Play));

    }


    IEnumerator Play()
    {
        isGameStarted = true;
        startUI.SetActive(false);
        yield return new WaitForSeconds(1);
        playUI.SetActive(true);
        LevelManager.Instance.LoadNewLevel();

    }

    public void GameFailed()
    {
        isGameFailed = true;
    }

    public void LevelComplete()
    {
        UIManager.Instance.isGameFailed = false;
        UIManager.Instance.isGameSuccesful = true;
        playUI.SetActive(false);
    }

    public void WriteScore()
    {
        scoreText.text = ScoreManager.Instance.GetTotalScore().ToString();
        levelFailedScoreText.text = ScoreManager.Instance.GetTotalScore().ToString();

    }

    public void WriteStage()
    {
        stageText.text = "STAGE "+ (LevelManager.Instance.GetActiveStageIndex()+1).ToString();
    }


}
