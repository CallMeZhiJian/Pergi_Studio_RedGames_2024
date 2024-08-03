using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public GameObject[] terrainPrefabs; // Array of terrain prefabs to spawn
    public Transform playerTransform; // Reference to the player transform
    public float spawnDistance = 60.0f; // Distance ahead of the player to spawn new terrain
    public float terrainLength = 20.0f; // Length of each terrain piece

    private float nextSpawnZ; // Z position to spawn the next terrain piece

    void Start()
    {
        // Initialize the next spawn position
        nextSpawnZ = playerTransform.position.z + spawnDistance;
        // Spawn initial terrain pieces
        for (int i = 0; i < 5; i++)
        {
            SpawnTerrain();
        }
    }

    void Update()
    {
        // Check if the player has moved close enough to the next spawn position
        if (playerTransform.position.z + spawnDistance > nextSpawnZ)
        {
            SpawnTerrain();
        }
    }

    void SpawnTerrain()
    {
        // Choose a random terrain prefab
        int randomIndex = Random.Range(0, terrainPrefabs.Length);
        GameObject terrain = Instantiate(terrainPrefabs[randomIndex], new Vector3(0, -3, nextSpawnZ), Quaternion.identity);
        nextSpawnZ += terrainLength;
    }
}
