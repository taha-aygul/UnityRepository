using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    private PlayerController _playerController;
    public Terrain terrain;
    public GameObject[] zombiePrefabs;
    public float maxZombieCount;
    public float zombieSpawnTime;
    private float timer;
    public float spawnTimeIncreaser;
    public float maxDistance;
    public float minRadiusFromPlayer;
    public float maxRadiusFromPlayer;

    public float minimumDistanceToBorder;
    public List<GameObject> Zombies;
    public static ZombieSpawnManager Instance;


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
        _playerController = PlayerController.Instance;

        StartCoroutine(ZombieSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer = Time.realtimeSinceStartup;

    }

    private IEnumerator ZombieSpawner()
    {

        if (Zombies.Count < maxZombieCount)
        {
            SpawnZombie();
        }


        if (timer > spawnTimeIncreaser && zombieSpawnTime > 2)
        {
            zombieSpawnTime--;
            spawnTimeIncreaser += spawnTimeIncreaser;

        }
        yield return new WaitForSeconds(zombieSpawnTime);
        StartCoroutine(ZombieSpawner());

    }

    private void SpawnZombie()
    {
        print(terrain.terrainData.bounds.min.x + " "+ terrain.terrainData.bounds.max.x);
        print(terrain.terrainData.bounds.min.z + " " + terrain.terrainData.bounds.max.z);

        int RandomPosX = Convert.ToInt32(UnityEngine.Random.Range(terrain.terrainData.bounds.min.x + minimumDistanceToBorder, terrain.terrainData.bounds.max.x - minimumDistanceToBorder));
        int RandomPosZ = Convert.ToInt32(UnityEngine.Random.Range(terrain.terrainData.bounds.min.z + minimumDistanceToBorder, terrain.terrainData.bounds.max.z - minimumDistanceToBorder));
        float height = terrain.terrainData.GetHeight(RandomPosX, RandomPosZ);
        Vector3 point = new Vector3(RandomPosX, height, RandomPosZ);

        bool isTooCloseToPlayer = Vector3.Distance(point, _playerController.transform.position) < minRadiusFromPlayer;
        bool isTooFarFromPlayer = Vector3.Distance(point, _playerController.transform.position) > maxRadiusFromPlayer;


        while (isTooCloseToPlayer || isTooFarFromPlayer)
        {
            RandomPosX = Convert.ToInt32(UnityEngine.Random.Range(terrain.terrainData.bounds.min.x + minimumDistanceToBorder, terrain.terrainData.bounds.max.x - minimumDistanceToBorder));
            RandomPosZ = Convert.ToInt32(UnityEngine.Random.Range(terrain.terrainData.bounds.min.z + minimumDistanceToBorder, terrain.terrainData.bounds.max.z - minimumDistanceToBorder));
            height = terrain.terrainData.GetHeight(RandomPosX, RandomPosZ);
            point = new Vector3(RandomPosX, height, RandomPosZ);

            isTooCloseToPlayer = Vector3.Distance(point, _playerController.transform.position) < minRadiusFromPlayer;
            isTooFarFromPlayer = Vector3.Distance(point, _playerController.transform.position) > maxRadiusFromPlayer;
        }

        int index = UnityEngine.Random.Range(0, zombiePrefabs.Length);
        Zombies.Add(Instantiate(zombiePrefabs[index], point, Quaternion.identity));

    }

    public void ClearZombies()
    {
        Zombies.Clear();
    }



}
