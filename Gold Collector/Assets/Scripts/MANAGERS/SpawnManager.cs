using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject level, gold, jumpBonus, speedBonus, spawnersParent;
    public int goldRate, jumpBonuRate, speedBonusRate;
    private Transform[] childsTransform, spawnersTransform;
    private ArrayList spawners;
    private Vector3 spawnPoint;
    private GameObject _nextObject;
    public float spawnInterval;
    private float _timeCounter;

    //private List<Transform> spawners;

    // Start is called before the first frame update
    void Start()
    {

        jumpBonuRate += goldRate;
        speedBonusRate += jumpBonuRate;
        spawners = new ArrayList();
        spawnersTransform = spawnersParent.GetComponentsInChildren<Transform>();

        //GetSpawners();

        //Platforms = GameObject.Find("Platforms");
        // GetSpawnersWithFor();
    }

    // Update is called once per frame
    void Update()
    {
        _timeCounter += Time.deltaTime;

        if (_timeCounter >= spawnInterval)
        {
            GetSpawnPoint();
            GetSpawnObject();
            Spawn();
            _timeCounter = 0;
        }




    }





    private void GetSpawnPoint()
    {
        /* 
         * spawnersTransform[0]
           spawnersTransform[1] 
           spawnersTransform[2] 
           spawnersTransform[3] 
           spawnersTransform[4] 
           spawnersTransform[5] 

        */
        int r1 = Random.Range(1, spawnersTransform.Length);
        int r2 = Random.Range(1, spawnersTransform.Length);
        float X1 = spawnersTransform[r1].position.x;
        float X2 = spawnersTransform[r2].position.x;
        float Y = spawnersTransform[r2].position.y;
        float Z1 = spawnersTransform[r1].position.z;
        float Z2 = spawnersTransform[r2].position.z;



        if (spawnersTransform[r1].position == spawnersTransform[r2].position)  // veya direk r1 == r2 denebilir
        {
            GetSpawnPoint();
        }
        else if (X1 == X2)
        {
            spawnPoint = new Vector3(X1, Y, Random.Range(Z1, Z2));
        }
        else if (Z1 == Z2)
        {
            spawnPoint = new Vector3(Random.Range(X1, X2), Y, Z1);
        }
        else
        {
            GetSpawnPoint();
        }




    }

    private void GetSpawnObject()
    {
        int random = Random.Range(0, speedBonusRate);

        if (random < goldRate)
        {
            _nextObject = gold;
        }
        else if (random < jumpBonuRate)
        {
            _nextObject = jumpBonus;
        }
        else if (random < speedBonusRate)
        {
            _nextObject = speedBonus;
        }



    }

    private void Spawn()
    {
        if (_nextObject != null)
        {
            Instantiate(_nextObject, spawnPoint, Quaternion.identity);
        }
    }





    private void GetSpawners()
    {
        for (int i = 0; i < spawnersParent.transform.childCount; i++)
        {
            spawners.Add(spawnersParent.transform.GetChild(i));
            print(spawners[i]);
        }

    }



    private void GetSpawnersWithFor()
    {
        childsTransform = level.GetComponentsInChildren<Transform>();

        for (int i = 0; i < childsTransform.Length; i++)
        {
            if (childsTransform[i] != null)
            {

                if (childsTransform[i].name.Equals("EV_Platform-CamTarget") || childsTransform[i].name.Equals("EV_Catwalk"))
                {
                    //  spawnersTransform[j] = childsTransform[i];
                    //  j++;
                    spawners.Add(childsTransform[i]);
                }

            }

            /* if (childsTransform[i].CompareTag("Respawn"))
             {
                 spawners.Add(childsTransform[i]);
             }*/
        }
    }
}
