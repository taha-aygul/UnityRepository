using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    public Terrain terrain;

    public static TerrainManager Instance;

    private void Awake()
    {
        MakeSingleton();
    }

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public float GetTerrainBounds_minX()
    {
        return terrain.terrainData.bounds.min.x;
    }
    public float GetTerrainBounds_minZ()
    {
        return terrain.terrainData.bounds.min.z;
    }

    public float GetTerrainBounds_maxX()
    {
        return terrain.terrainData.bounds.max.x;
    }
    public float GetTerrainBounds_maxZ()
    {
        return terrain.terrainData.bounds.max.z;
    }

    public float GetHeight(int x , int z)
    {
        return terrain.terrainData.GetHeight(x,z);
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
