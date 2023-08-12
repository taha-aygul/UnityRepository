using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject fillPrefab;
    public Transform[] allCells;


    public static SpawnManager Instance;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Spawn();
        }
    }

    public void Spawn()
    {

        float chance = Random.Range(0,1f);
        int whichSpawn = Random.Range(0, allCells.Length);

        if(allCells[whichSpawn].childCount != 0)
        {
            Spawn();
            return;
        }


        if (chance < 0.2f)
        {
            return;
        }
        else if(chance < 0.8f)
        {
            GameObject fillGO = Instantiate(fillPrefab, allCells[whichSpawn]);
            Debug.Log("2");
            Fill tempFill = fillGO.GetComponent<Fill>();
            tempFill.FillValueUpdate(2);
            // fillGO.GetComponent<Fill>().FillValueUpdate(2);

        }
        else
        {
            GameObject fillGO = Instantiate(fillPrefab, allCells[whichSpawn]);
            Debug.Log("4");

            Fill tempFill = fillGO.GetComponent<Fill>();
            tempFill.FillValueUpdate(2);
        }

    }

    public Transform[] getAllCells()
    {
        return allCells;
    }
}
