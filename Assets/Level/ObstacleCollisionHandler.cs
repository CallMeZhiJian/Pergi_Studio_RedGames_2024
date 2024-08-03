using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollisionHandler : MonoBehaviour
{
    private LevelManager levelManager; // Reference to the GameManager

    void Start()
    {
        // Find the GameManager object
        GameObject manager = GameObject.FindWithTag("LevelManager");
        if (manager != null)
        {
            levelManager = manager.GetComponent<LevelManager>();
        }
        else
        {
            Debug.LogError("LevelManager not found!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with the obstacle
        if (other.gameObject.CompareTag("Player"))
        {
            if (levelManager != null)
            {
                levelManager.GameOver();
            }
        }
    }
}
