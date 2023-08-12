using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public float totalTimeToFinishGame;
    public float currentTime;
    public bool isGameFinished;

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
        currentTime = totalTimeToFinishGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameFinished && UIManager.Instance.gameStarted)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 10)
            {
                UIManager.Instance.timerText.color = Color.red; //new Color();
            }


            if (currentTime <= 0)
            {
                isGameFinished = true;
                Invoke(nameof(DestroyBalloons), 2f);

                //UIManager.Instance.EndGame();
            }
            UIManager.Instance.UpdateTime();
        }
    }
    public void DestroyBalloons()
    {
        UIManager.Instance.EndGame();
    }
}
