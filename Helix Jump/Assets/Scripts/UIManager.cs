using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject startUI, finishUI, playingUI, deadUI;
    public TextMeshProUGUI levelText;
    public bool isGameStarted;
    int levelIndex = 1;
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
        levelIndex++;
        String level = "Level" + levelIndex;
        SceneManager.LoadScene(level);

    }
    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
