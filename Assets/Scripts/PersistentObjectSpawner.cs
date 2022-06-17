using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    public GameObject persistentObjectPrefab;
    static bool hasSpawned = false;

    private void Awake()
    {
        if (hasSpawned) return;

        hasSpawned = true;
        SpawnPersistentObjects();
    }

    private void SpawnPersistentObjects()
    {
        GameObject persistentObject = Instantiate(persistentObjectPrefab);
        DontDestroyOnLoad(persistentObject);
    }
}
