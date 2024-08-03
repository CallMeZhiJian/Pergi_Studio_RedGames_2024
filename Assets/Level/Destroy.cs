using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name);

        // Check if the colliding object is tagged as "Terrain"
        if (other.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("Collide with Terrain: " + other.gameObject.name);
            // Destroy the terrain GameObject
            Destroy(other.gameObject);
        }
    }
}

