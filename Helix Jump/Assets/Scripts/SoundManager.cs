using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] BallController ball;
    [SerializeField] AudioSource audioSource;
    [SerializeField]private List<AudioClip> ballBounces = new List<AudioClip>();
    [SerializeField] private List<AudioClip> backThemeMusic = new List<AudioClip>();
    [SerializeField] private List<AudioClip> circleBreaks = new List<AudioClip>();
    private AudioClip currentBallBounce;
    private AudioClip currentBackThemeMusic;
    private AudioClip currentCircleBreaks;

    public static SoundManager Instance;
    private void Awake()
    {
        MakeSingleton();
        RandomBonuceAudio();
        RandomBackThemeMusic();
    }

  


    public void RandomBonuceAudio()
    {
        int r = Random.Range(0, ballBounces.Count-1);
        currentBallBounce = ballBounces[r];
    }
    public void PlayBallBounce()
    {
        audioSource.PlayOneShot(currentBallBounce);
    }





    public void RandomBackThemeMusic()
    {
        int r = Random.Range(0, ballBounces.Count-1);
        if (backThemeMusic != null)
        {
        //currentBackThemeMusic = backThemeMusic[r];
        }
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

}
