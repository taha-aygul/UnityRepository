using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{

    public static PlayerLevelManager Instance;
    [SerializeField] private int currentLevel;
    [SerializeField] private float firstLevelXP;
    [SerializeField] private float currentLevelXP;
    [SerializeField] private float currentXP;
    private float nextLevelXP;

    public int CurrentLevel { get => currentLevel;  set => currentLevel = value; }
    public float CurrentXP { get => currentXP; set => currentXP = value; }
    public float CurrentLevelXP { get => currentLevelXP; set => currentLevelXP = value; }

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
        currentLevelXP = firstLevelXP;
    }

    public void AddXP()
    {
        currentXP++;
        CheckXP();

    }
    public void AddXP(int XP)
    {
        currentXP += XP;
        CheckXP();
    }

    private void CheckXP()
    {
        if (currentXP >= currentLevelXP)
        {
            LevelUP();
        }

        UIManager.Instance.UpdateLevelBar();
    }

    private void LevelUP()
    {
        currentXP -= currentLevelXP;
        nextLevelXP = currentLevelXP + CurrentLevelXP * 0.25f;
        currentLevelXP =  Mathf.RoundToInt (nextLevelXP);
        currentLevel++;
        CheckXP();
    }



    public void ShootingLevelUp()
    {

    }
}
