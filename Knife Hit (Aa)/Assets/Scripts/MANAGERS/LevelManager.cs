using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    //  LEVELLER STAGELERDEN OLU�UR 


    public List<GameObject> levels = new List<GameObject>();
    [SerializeField]private GameObject activeLevel;
    [SerializeField]private Transform activeStage;
    private int nextStageIndex = 0, nextLevelIndex = 0;

    GameObject activeLevelParent;
       


public static LevelManager Instance;

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
        activeLevelParent = new GameObject();
        activeLevelParent.name = "Active Level";
    }
    public void LoadNewLevel()
    {
        nextStageIndex = 0;
        if(activeLevel != null)
        {
            activeLevel.gameObject.SetActive(false); // Destroy(activeLevel)

        }
       
        activeLevel = Instantiate(levels[nextLevelIndex], activeLevelParent.transform);       // levels[nextLevelIndex];
        activeLevel.gameObject.SetActive(true);
       // print(nextLevelIndex);
        nextLevelIndex++;
        LoadNewStage();

    }

    public void LoadNewStage()
    {
        if (activeStage != null)
        {
            activeStage.gameObject.SetActive(false);
        }

        if (nextStageIndex >= activeLevel.transform.childCount)                     // Child count 5 verir  buraya leveli arttıracalk şeyi koy
        {
            Invoke(nameof(LoadNewLevel), 1f);
            print("Level Complete");
            //LoadNewLevel(); // LEVEL COMPLETE YAZMALI
        }
        UIManager.Instance.WriteStage();
        activeStage = activeLevel.transform.GetChild(nextStageIndex);
        activeStage.gameObject.SetActive(true);
        nextStageIndex++;

        KnifeManager.Instance.RefreshStage(activeStage);
    }


    public void ReloadCurrentLevel()
    {
        Destroy(activeLevel);
        nextLevelIndex--;
        LoadNewLevel();

    }


    public int GetActiveStageIndex()
    {
        return nextStageIndex ;
    }

   

    public Transform GetActiveStageKnifeParent()
    {
        return activeStage.GetChild(1);
    } 
    /*public int GetActiveStageKnifeCount()
    {
        return activeStage.Find("KnifeParent").childCount;
    }*/
}
