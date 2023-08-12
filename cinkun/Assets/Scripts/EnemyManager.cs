using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] GameObject slime;
    [SerializeField] GameObject pig;
    [SerializeField] GameObject bear;
    [SerializeField] GameObject frog;
    // Start is called before the first frame update
    void Start()
    {
        
        Create(frog);
        Create(frog);
        Create(frog);
        Create(frog);
        Create(frog);
        Create(frog);
        Create(frog);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Create(GameObject enemy)
    {
        Vector3 spawnPoint = new Vector3(/*Random.Range(-380, -400)*/-430, -118/*Random.Range(-145, -140)*/, 0);
        Instantiate(enemy, spawnPoint, Quaternion.identity);
        Debug.LogError("Instantiated");
    }
}

