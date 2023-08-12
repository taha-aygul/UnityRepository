using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject StartGame;
    [SerializeField] GameObject Level1;
    [SerializeField] GameObject slime;
    [SerializeField] GameObject pig;
    [SerializeField] GameObject bear;
    [SerializeField] GameObject frog;
    void Start()
    {
        Create(slime,55);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartGame.SetActive(false);
            Level1.SetActive(true);
        }

    }

    private void Create(GameObject enemy , int count)
    {
        int i = 0;
        while (i < count)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-440, -420), Random.Range(-130, -100), 0);
            Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
        
       // Debug.LogError("Instantiated");
    }


    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
