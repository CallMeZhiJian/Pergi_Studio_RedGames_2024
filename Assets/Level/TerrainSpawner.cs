using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public GameObject[] terrainPrefabs; 
    public Transform playerTransform; 
    public float spawnDistance = 90.0f; 
    public float terrainLength = 20.0f; 
    private float nextSpawnZ; 

    void Start()
    {
        nextSpawnZ = playerTransform.position.z + spawnDistance;
        for (int i = 0; i < 5; i++)
        {
            SpawnTerrain();
        }
    }

    void Update()
    {
        if (playerTransform.position.z + spawnDistance > nextSpawnZ)
        {
            SpawnTerrain();
        }
    }

    void SpawnTerrain()
    {
        int randomIndex = Random.Range(0, terrainPrefabs.Length);
        GameObject terrain = Instantiate(terrainPrefabs[randomIndex], new Vector3(0, -1.45f, nextSpawnZ), Quaternion.identity);
        nextSpawnZ += terrainLength;
    }
}
