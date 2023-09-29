using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorder : MonoBehaviour
{

    [SerializeField] private string level = "Level";
    [SerializeField] private string theme = "Theme";
    [SerializeField] private string CircleCount = "CircleCount";
    [SerializeField] private string MaxEmptyCount = "MaxEmptyCount";
    [SerializeField] private string hasRedPieces = "HasRedPieces";
    [SerializeField] private string redCount = "RedCount";
    [SerializeField] private string hasMovingPieces = "HasMovingPieces";
    [SerializeField] private string movingPieceFrequency = "MovingPieceFrequency";


    public static DataRecorder Instance;


    private void Awake()
    {

        MakeSingleton();

        if (PlayerPrefs.HasKey(level))
        {
            print("load");
            LoadLevel();
            LevelCreator.Instance.CreateLevel();
            UIManager.Instance.UpdateLevel();
        }
        else
        {
            print("start");
            LevelCreator.Instance.CreateLevel();
            UIManager.Instance.UpdateLevelAtStart();
        }
    }
    private void Start()
    {
        UIManager.Instance.UpdateLevelTextAndColor();

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

    public void Save(int lastLevel)
    {
        print("saved " + lastLevel + " t:" + ColorManager.Instance.index);
        SaveLevel(lastLevel);
        PlayerPrefs.Save();
    }


    public void SaveLevel(int lastLevel)
    {

        PlayerPrefs.SetInt(level, lastLevel - 1);
        PlayerPrefs.SetInt(MaxEmptyCount, LevelCreator.Instance.maxEmptyCount);
        PlayerPrefs.SetInt(CircleCount, LevelCreator.Instance.circleCount);
        PlayerPrefs.SetInt(redCount, LevelCreator.Instance.redCount);
        PlayerPrefs.SetInt(movingPieceFrequency, LevelCreator.Instance.movingPieceFrequency);
        PlayerPrefs.SetInt(hasRedPieces, boolToInt(LevelCreator.Instance.hasRedPieces));
        PlayerPrefs.SetInt(hasMovingPieces, boolToInt(LevelCreator.Instance.hasMovingPieces));
        PlayerPrefs.SetInt(theme, ColorManager.Instance.index);

    }

    public void LoadLevel()
    {
        UIManager.Instance.currentLevel = PlayerPrefs.GetInt(level);
        LevelCreator.Instance.maxEmptyCount = PlayerPrefs.GetInt(MaxEmptyCount);
        LevelCreator.Instance.circleCount = PlayerPrefs.GetInt(CircleCount);
        LevelCreator.Instance.redCount = PlayerPrefs.GetInt(redCount);
        LevelCreator.Instance.movingPieceFrequency = PlayerPrefs.GetInt(movingPieceFrequency);
        LevelCreator.Instance.hasRedPieces = intToBool(PlayerPrefs.GetInt(hasRedPieces));
        LevelCreator.Instance.hasMovingPieces = intToBool(PlayerPrefs.GetInt(hasMovingPieces));
        ColorManager.Instance.index = PlayerPrefs.GetInt(theme);
        //LoadTheme();
    }


   

    public int LoadTheme()
    {
        if (PlayerPrefs.HasKey(theme))
        {
            print("loaded " + PlayerPrefs.GetInt(theme) + " " +theme);
            return PlayerPrefs.GetInt(theme);
        }
        return 0;
    }

    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }




}
