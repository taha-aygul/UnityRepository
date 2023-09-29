using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private
        GameObject mainMenuUI, pauseMenuUI, endMenuUI, playingUI;

    [SerializeField]
    private
        TextMeshProUGUI timeTxt, scoreTxt,highScoreTxt, newHighscoreTxt, starTxt;


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
           
            timeTxt.text =   TimeAndScoreManager.Instance.Time.ToString("F1");
        }
    }

    public void UpdateStarCount()          // Calling in playerController
    {
            starTxt.text =   TimeAndScoreManager.Instance.StarCount.ToString();
    }


    public void UpdateScore()               
    {
        scoreTxt.text = TimeAndScoreManager.Instance.ScoreTotal.ToString();
        highScoreTxt.text = TimeAndScoreManager.Instance.HighScore.ToString();
        if (TimeAndScoreManager.Instance.NewHighScore)
        {
            newHighscoreTxt.text = "New HighScore!";
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
       // highScoreTxt.text = TimeAndScoreManager.Instance.ScoreTotal.ToString();


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
