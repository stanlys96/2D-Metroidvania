using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public AudioSource level1Audio;

    // Start is called before the first frame update
    void Start()
    {
        level1Audio.Play();
    }
}
