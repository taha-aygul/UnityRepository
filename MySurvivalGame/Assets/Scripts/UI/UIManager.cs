using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private
        GameObject mainMenuUI, pauseMenuUI, endMenuUI, playingUI;

    [SerializeField]
    private
        TextMeshProUGUI healthTxt, timeTxt, scoreTxt, highScoreTxt, newHighscoreTxt;

    [SerializeField]
    private Image healthBar, timeBar;

    public static UIManager Instance;
    private GameManager _gameManager;

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
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }


    public void UpdateTime()
    {
        if (!_gameManager.IsGameOver() || !_gameManager.IsGamePaused())
        {
            string timeAsMinute;
            int time = (int)Mathf.Round(TimeAndScoreManager.Instance.Time);

            if (time % 60 >= 10)
            {
                timeAsMinute = time / 60 + "." + time % 60;
            }
            else
            {
                timeAsMinute = time / 60 + ".0" + time % 60;
            }
            timeTxt.text = timeAsMinute;
            timeBar.fillAmount = TimeAndScoreManager.Instance.Time / TimeAndScoreManager.Instance.FirstRemainingTime;
        }
    }

    public void UpdateHealth()
    {
        healthTxt.text = PlayerController.Instance.Health.ToString();
        healthBar.fillAmount =    PlayerController.Instance.Health / 100;
    }

    public void UpdateScore()
    {
        scoreTxt.text = "Score \n"+TimeAndScoreManager.Instance.ScoreTotal.ToString();
        highScoreTxt.text = "Highscore \n"+TimeAndScoreManager.Instance.HighScore.ToString();
        if (TimeAndScoreManager.Instance.NewHighScore)
        {
            newHighscoreTxt.text = "New HighScore!!";
        }
    }

    public void OpenMainMenu()
    {
        mainMenuUI.SetActive(true);
    }
    public void CloseMainMenu()
    {
        mainMenuUI.SetActive(false);
    }

    public void OpenEndMenu()
    {
        endMenuUI.SetActive(true);
        scoreTxt.text = TimeAndScoreManager.Instance.ScoreTotal.ToString();
        UpdateScore();
    }
    public void CloseMenu()
    {
        endMenuUI.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        pauseMenuUI.SetActive(false);
    }

    public void OpenPlayingUI()
    {
        playingUI.SetActive(true);
    }
    public void ClosePlayingUI()
    {
        playingUI.SetActive(false);
    }


}
