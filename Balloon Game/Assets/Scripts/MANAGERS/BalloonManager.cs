using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    public GameObject greenBalloon, blueBalloon, redBalloon, blueExplosiveBalloon, greenExplosiveBalloon;
    private GameObject _nextBalloon;
    
    public Vector3 minPoint;
    public Vector3 maxPoint;
    private Vector3 _scaleOfBalloon;
    public float baloonSizeMax, baloonSizeMin;
    public float balloonSpawnIntervalMinValue;
    public float balloonSpawnIntervalMaxValue;
    private float _nextBalloonSpawnInterval;
    private float _balloonSpawnTime;
    private float _random, _totalPossibility ;
    public float greenPossibility, bluePossibility, redPossibility, greenExplosivePossibility, blueExplosivePossibility;
    public float _totalPoint;
    public int greenScore, blueScore, explosionMinusScore;

    public static BalloonManager Instance;
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
        _totalPossibility = greenPossibility + bluePossibility + redPossibility + greenExplosivePossibility + blueExplosivePossibility;
        bluePossibility += greenPossibility;
        redPossibility += bluePossibility;
        greenExplosivePossibility += redPossibility;
        blueExplosivePossibility += greenExplosivePossibility;

        ReassignNextBalloonSpawnInterval();
    }

    // Update is called once per frame
    void Update()
    {

        if (!TimerManager.Instance.isGameFinished && UIManager.Instance.playingUIPanel.activeInHierarchy)
        {
            _balloonSpawnTime += Time.deltaTime;
            if (_balloonSpawnTime >= _nextBalloonSpawnInterval)
            {
                SpawnBalloon();
                _balloonSpawnTime = 0;
                ReassignNextBalloonSpawnInterval();
            }
        }
    }

    private void DetectNextBalloon() 
    {
        
        _random = Random.Range(0,_totalPossibility);

        if(_random < greenPossibility)
        {
            _nextBalloon = greenBalloon;
            _totalPoint += greenScore;
        } 
        else if (_random < bluePossibility )
        {
            _nextBalloon = blueBalloon;
            _totalPoint += blueScore;
        }
        else if (_random < redPossibility)
        {
            _nextBalloon = redBalloon;
        }
        else if (_random < greenExplosivePossibility)
        {
            _nextBalloon = greenExplosiveBalloon;
        }
        else if (_random < blueExplosivePossibility)
        {
            _nextBalloon = blueExplosiveBalloon;
        }
       



    }

    private void SpawnBalloon()
    {

        DetectNextBalloon();
        ResizeBalloon();
        if (_nextBalloon.CompareTag("Explosive") || _nextBalloon.CompareTag("RedBalloon"))
        {
            _scaleOfBalloon -= new Vector3(0.04f, 0.04f, 0.04f);
        }
        _nextBalloon.transform.localScale = _scaleOfBalloon;
        Instantiate(_nextBalloon, new Vector2(Random.Range(minPoint.x, maxPoint.x), minPoint.y), Quaternion.identity);
    }

    private void ReassignNextBalloonSpawnInterval()
    {
        _nextBalloonSpawnInterval = Random.Range(balloonSpawnIntervalMinValue, balloonSpawnIntervalMaxValue);
    }
    private void ResizeBalloon()
    {
        float randomSize = Random.Range(baloonSizeMin, baloonSizeMax);
        _scaleOfBalloon = new Vector3(randomSize,randomSize,randomSize);
    }
}
