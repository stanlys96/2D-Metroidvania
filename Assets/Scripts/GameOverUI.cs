using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    static bool hasSpawned = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (!hasSpawned)
        {
            hasSpawned = true;
            DontDestroyOnLoad(gameObject);
        }
    }
}
