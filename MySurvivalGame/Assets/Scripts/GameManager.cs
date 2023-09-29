using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private bool _gameOver, _gamePaused, _gameStarted;
    private UIManager UIManager;

    public bool GameStarted { get => _gameStarted; set => _gameStarted = value; }

    private void Awake()
    {
        _gamePaused = true; // Oyun baþý menüsü
        _gameStarted = false;
        _gameOver = false;
        MakeSingleton();
    }


    private void Start()
    {
        Time.timeScale = 0f;
        UIManager = UIManager.Instance;
       // StartGame();
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

        ZombieSpawnManager.Instance.ClearZombies();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
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
        Cursor.lockState = CursorLockMode.Confined;
        _gamePaused = true;
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        PlayerController.Instance.ToggleControl();
        UIManager.ClosePauseMenu();
        _gamePaused = false;
        Time.timeScale = 1f;

    }
    public void StartGame()
    {
        PlayerController.Instance.ToggleControl();
        UIManager.OpenPlayingUI();
        UIManager.CloseMainMenu();
        Time.timeScale = 1f;
       _gameStarted = true;
        _gamePaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
