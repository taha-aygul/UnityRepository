using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI totalCount;
    public TextMeshProUGUI currentCount;
    public static UIManager Instance;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCount.text = "CURRENT : " +TailController.Instance.CurrentTailCount();
        totalCount.text = "TOTAL : " + TailController.Instance.TotalTailCount();
    }
}
