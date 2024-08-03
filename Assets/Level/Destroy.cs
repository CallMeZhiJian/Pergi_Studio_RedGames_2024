using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        // Check if the colliding object is tagged as "Terrain"
        if (other.gameObject.CompareTag("Terrain"))
        {
            // Destroy the terrain GameObject
            Destroy(other.gameObject);
        }
    }
}

