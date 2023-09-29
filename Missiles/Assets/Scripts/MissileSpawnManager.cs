using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> missiles = new List<GameObject>();
    [SerializeField] private float spawnTimeInterval;
    [SerializeField] private Vector2 rangeSpawnDistance;
    private Vector3 randomPosition;
    private GameObject missile;
    private float missileCount = 2, counter;


    public static MissileSpawnManager Instance;

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

    void Start()
    {
        StartCoroutine(nameof(SpawnMissiles));
    }




    private IEnumerator SpawnMissiles()
    {

        for (int i = 0; i < missileCount; i++)
        {
            randomPosition = PlayerController.Instance.transform.position + (Vector3)(Random.insideUnitCircle.normalized * Random.Range(rangeSpawnDistance.x, rangeSpawnDistance.y));
            SelectMissile();
            GameObject Missile = Instantiate(missile, randomPosition, Quaternion.identity);
        }

        yield return new WaitForSeconds(spawnTimeInterval);

        if (spawnTimeInterval >= 0.15f)
        {
        spawnTimeInterval -= 0.1f;
        }

        if (spawnTimeInterval % 0.5f == 0)
        {
            missileCount++;
        }
        else
        {
            missileCount = Random.Range(missileCount - 1, missileCount + 1);
        }
        StartCoroutine(nameof(SpawnMissiles));

    }


    private void SelectMissile()
    {
        missile = missiles[0];
    }


    public void StopSpawning()
    {
        StopAllCoroutines();
        spawnTimeInterval = 0;

    }

}
