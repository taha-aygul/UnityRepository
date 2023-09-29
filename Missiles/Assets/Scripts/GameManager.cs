using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;
    private bool _gameOver, _gamePaused;
    private UIManager UIManager;


    private void Awake()
    {
        _gamePaused = true; // Oyun baþý menüsü
        _gameOver = false;
        MakeSingleton();
    }


    private void Start()
    {
        Time.timeScale = 0f;
        UIManager = UIManager.Instance;
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


    public bool IsGameOver()
    {
        return _gameOver;
    }
    public void GameOver()
    {
        _gameOver = true;
        TimeAndScoreManager.Instance.UpdateScore();
        UIManager.OpenEndMenu();
        UIManager.ClosePlayingUI();
        Invoke(nameof(StopTime),1f);

    }
    public bool IsGamePaused()
    {
        return _gamePaused;
    }

    public void PauseGame()
    {
        UIManager.OpenPauseMenu();
        _gamePaused = true;
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        UIManager.ClosePauseMenu();
        _gamePaused = false;
        Time.timeScale = 1f;

    }
    public void StartGame()
    {
        UIManager.OpenPlayingUI();
        UIManager.CloseMainMenu();
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }
}
