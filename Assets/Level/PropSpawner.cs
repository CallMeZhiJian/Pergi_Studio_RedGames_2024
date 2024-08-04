using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public GameObject[] props;
    public Transform[] spawnPoints;
    private float[] lanePositions = { -2.5f, 0.0f, 2.5f };

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            List<float> availableLanes = new List<float>(lanePositions);
            ShuffleList(availableLanes);

            List<GameObject> availableProps = new List<GameObject>(props);
            ShuffleList(availableProps);

            List<GameObject> spawnedProps = new List<GameObject>();

            for (int i = 0; i < Mathf.Min(availableLanes.Count, availableProps.Count); i++)
            {
                Vector3 spawnPosition = spawnPoint.position + new Vector3(availableLanes[i], 0, 0);
                GameObject prop = Instantiate(availableProps[i], spawnPosition, Quaternion.identity);
                spawnedProps.Add(prop);
                prop.AddComponent<PropCollisionHandler>().Initialize(spawnedProps); 
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
