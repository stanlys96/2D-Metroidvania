using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] GameObject childObject;

    void Update()
    {
        childObject.transform.position = gameObject.transform.position;
    }
}
