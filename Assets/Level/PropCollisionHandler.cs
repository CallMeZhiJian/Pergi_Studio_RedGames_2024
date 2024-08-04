using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCollisionHandler : MonoBehaviour
{
    private List<GameObject> propGroup;

    public void Initialize(List<GameObject> group)
    {
        propGroup = group;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyAllPropsInGroup();
        }
    }

    private void DestroyAllPropsInGroup()
    {
        foreach (GameObject prop in propGroup)
        {
            Destroy(prop);
        }
    }
}

