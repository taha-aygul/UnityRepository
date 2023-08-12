using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    public GameObject coin;

    public float Score = 0;
    
    private void OnTriggerEnter()
    {
        Score += 10;
        Debug.Log(Score);

    }

    public  float GetScore()
    {
        return Score;
    }
}
