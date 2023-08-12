using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject slime;
    [SerializeField] GameObject pig;
    [SerializeField] GameObject bear;
    [SerializeField] GameObject frog;

    void Start()
    {
        Create(slime);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Create(GameObject enemy)
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-6, -420), Random.Range(-130, -100), 0);
        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }
}
