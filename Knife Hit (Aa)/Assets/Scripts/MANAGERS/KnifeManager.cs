using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    [SerializeField] private GameObject currentKnife;
    [SerializeField] private int knifeCount;
    // [SerializeField] private List<Transform> knifeList = new List<Transform>();
    [SerializeField] private Transform[] knifeList;

    [SerializeField] private List<GameObject> knifeThrowedList = new List<GameObject>();
    private GameObject knifesInStage;
    private int activatedKnifeIndex;
    public Vector3 knifeThrowPosition, knifeSpawnPosition;
    [Range(0, 10)] public float speed;



    #region ContextMenu
    [ContextMenu(nameof(CreateKnifes))]
    private void CreateKnifes()
    {
        CreateNewKnifeList();
    }
    #endregion


    public static KnifeManager Instance;

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



    private void CreateNewKnifeList()
    {
        GameObject knifeParent = new GameObject();
        knifeParent.name = "KnifeParent";

        for (int i = 0; i < knifeCount; i++)
        {
            GameObject knife = Instantiate(currentKnife, knifeSpawnPosition, Quaternion.identity);
            knife.transform.SetParent(knifeParent.transform);
            knife.SetActive(false);
           
        }
    }


    public void RefreshStage(Transform Stage)
    {
        if (UIManager.Instance.firstThrow)
        {
            activatedKnifeIndex = 0;
        }
        else
        {
            activatedKnifeIndex = 1;
        }
        knifeList = Stage.transform.GetChild(1).GetComponentsInChildren<Transform>(true);      // Inaktif objeleri de alýr  // GetChild(1) çünkü knifelar child 1 da olacak
        ActivateNextKnife();
        
    }

    public void ActivateNextKnife()
    {
        if (activatedKnifeIndex <= knifeList.Length-1)
        {
            knifeList[activatedKnifeIndex].gameObject.SetActive(true);
            activatedKnifeIndex++;

        }
        else
        {
            LevelManager.Instance.LoadNewStage();
        }
    }



    /*public void ClearKnifes()
    {
        for (int i = 0; i < knifeList.Count; i++)
        {
            DestroyImmediate(knifeList[i]);
        }
        knifeList.Clear();
    }*/


    /* private void CreateKnife()                // Sürekli eski listeyi silip yeni liste oluþturmak istersen kullan
   {
       ClearKnifes();
       for (int i = 0; i < knifeCount; i++)
       {
           GameObject knife = Instantiate(currentKnife, knifeSpawnPosition, Quaternion.identity);
           knifeList.Add(knife);
           knife.SetActive(false);
       }
   }*/


}
