using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public GameObject[] props; // Array to hold your prop prefabs
    public Transform[] spawnPoints; // Array to hold the spawn points on the terrain

    private float[] lanePositions = { -2.5f, 0.0f, 2.5f }; // Example lane positions (adjust as needed)

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Create a list of lane positions and shuffle them
            List<float> availableLanes = new List<float>(lanePositions);
            ShuffleList(availableLanes);

            // Create a list of props and shuffle it
            List<GameObject> availableProps = new List<GameObject>(props);
            ShuffleList(availableProps);

            // Spawn one prop at each lane position
            for (int i = 0; i < Mathf.Min(availableLanes.Count, availableProps.Count); i++)
            {
                Vector3 spawnPosition = spawnPoint.position + new Vector3(availableLanes[i], 0, 0);
                Instantiate(availableProps[i], spawnPosition, Quaternion.identity);
            }
        }
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
