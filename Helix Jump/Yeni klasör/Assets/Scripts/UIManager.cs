using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject startUI, finishUI, playingUI, deadUI;
    public TextMeshProUGUI slideToStart, levelText, comboText;
    public bool isGameStarted;
    public int currentLevel = 0;
    [SerializeField] int levelRecordingFrequency;
    public static UIManager Instance;
    [SerializeField] BallController ballController;
    [SerializeField] CameraFollower cameraFollower;
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

    private void Start()
    {
       // UpdateLevel();
    }

    private void Update()
    {

        

        if (ballController.comboCount >= ballController.comboBreakCount)
        {
            comboText.text = "X \n" + ballController.comboCount;
        }
        else
        {
            comboText.text = " ";
        }
    }

    public void UpdateLevelAtStart()
    {
        currentLevel++;
    }

    public void Checkpoint()
    {
        if (currentLevel % levelRecordingFrequency == 0)
        {
            DataRecorder.Instance.Save(currentLevel);
        }
    }

    public void UpdateLevel()
    {
        currentLevel++;

        if (currentLevel % levelRecordingFrequency == 0)
        {
            DataRecorder.Instance.Save(currentLevel);
        }
    }

    public void UpdateLevelTextAndColor()
    {
        levelText.text = "Level " + currentLevel;
        levelText.color = ColorManager.Instance.GetCurrentTheme().ball;//ballController.GetComponent<MeshRenderer>().material.color;
        comboText.color = ColorManager.Instance.GetCurrentTheme().ball;//ballController.GetComponent<MeshRenderer>().material.color; 
    }


    public void closeEntry()
    {
        startUI.SetActive(false);
        playingUI.SetActive(true);
        isGameStarted = true;
    }

    public void openDeadUI()
    {
        deadUI.SetActive(true);
    }
    public void openFinish()
    {
        playingUI.SetActive(false);
        finishUI.SetActive(true);
    }

    public void NextLevel()
    {
        currentLevel++;
        String level = "Level" + currentLevel;
        SceneManager.LoadScene(level);

    }
    public void Restart()
    {
        //  TODO
        // resetting ball position , moveTowards ile yap
        // LevelCreator point 0 la
        // sahnedeki çemberleri sil
       /* ballController.ActivateBall();
        ballController.MoveBallToStart();
        cameraFollower.GoToStartPosition();
        LevelCreator.Instance.ResetLevel();*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {

        Application.Quit();
    }

}
