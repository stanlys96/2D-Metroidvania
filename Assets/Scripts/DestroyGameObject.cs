using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public GameObject parentObject;

    public void DestroyThis()
    {
        Destroy(parentObject);
    }
}
