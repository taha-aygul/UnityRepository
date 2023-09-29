using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    [SerializeField] private int maxSpawnCount;
    [SerializeField] private float spawnInterval, spawnIntervalDecrease;
    [SerializeField] private float minDistance, maxDistance;
    public LinkedList<GameObject> e = new LinkedList<GameObject>();

    // [SerializeField] private LinkedList<GameObject> e = new LinkedList<GameObject>();
    [SerializeField] private List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] private List<int> Probabilities = new List<int>();            // Yapýlmade
    private List<int> refinedProbabilities = new List<int>();            // Yapýlmade

    private float nextDistance;
    private GameObject nextEnemy;
    private Vector3 nextEnemyPosition;
    private int totalPossibility;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Probabilities.Count; i++)
        {
            totalPossibility += Probabilities[i];
        }
        if (GameManager.Instance.EndlessGame)
        {
            StartCoroutine(nameof(Spawner));
        }
    }

    private IEnumerator Spawner()
    {
        int r = Random.Range(1, maxSpawnCount + 1);
        for (int i = 0; i < maxSpawnCount; i++)
        {
            Spawn();
        }
        yield return new WaitForSeconds(spawnInterval);
        if (spawnInterval >= 1f) { spawnInterval -= spawnIntervalDecrease; }
        StartCoroutine(nameof(Spawner));

    }


    private void Spawn()
    {
        nextDistance = Random.Range(minDistance, maxDistance);
        nextEnemyPosition = PlayerController.Instance.transform.position + Random.insideUnitSphere * nextDistance;
        nextEnemyPosition.y = 1;                                               // Düzenleme ister

        // DetectNextEnemy();
        nextEnemy = Enemies[0];

        Instantiate(nextEnemy, nextEnemyPosition, Quaternion.identity);
    }

    private void DetectNextEnemy()
    {
        // int r = Random.Range(0, Enemies.Count);
        int r = Random.Range(0, totalPossibility);
        for (int i = 0; i < refinedProbabilities.Count - 1; i++)
        {
            if (r <= refinedProbabilities[i + 1] && r > refinedProbabilities[i])
            {
                nextEnemy = Enemies[i];
                break;
            }
            else if (i == 0 && r <= refinedProbabilities[0])
            {
                nextEnemy = Enemies[0];
                break;
            }

        }
        nextEnemy = Enemies[r];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
