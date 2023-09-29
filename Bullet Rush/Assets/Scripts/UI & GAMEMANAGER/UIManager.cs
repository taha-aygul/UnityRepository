using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private GameManager _gameManager;


    [Header("StartMenu")]
    [SerializeField] private GameObject mainMenuUI;

    [Header("Playing UI")]
    [SerializeField] private GameObject playingUI;
    [SerializeField] private TextMeshProUGUI timeTxt, coinTxt, levelText;
    [SerializeField] private Image levelBar;

    [Header("LevelUpUI")]
    [SerializeField] private GameObject levelUpUI;
    [SerializeField] private Button b1, b2;

    [Header("EndMenu")]
    [SerializeField] private GameObject endMenuUI;
    [SerializeField]
    private
        TextMeshProUGUI scoreTxt, highScoreTxt, newHighscoreTxt;




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

            timeTxt.text = TimeAndScoreManager.Instance.Time.ToString("F1");
        }
    }

    public void UpdateLevelBar()
    {
        levelBar.fillAmount = Mathf.Lerp(levelBar.fillAmount, PlayerLevelManager.Instance.CurrentXP / PlayerLevelManager.Instance.CurrentLevelXP, 0.5f);
        levelText.text = PlayerLevelManager.Instance.CurrentLevel.ToString(); ;

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

    public void OpenLevelUpMenu()
    {
        levelUpUI.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        levelUpUI.SetActive(false);
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
