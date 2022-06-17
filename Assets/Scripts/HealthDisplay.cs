using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthDisplay : MonoBehaviour
{
    public RectTransform rectTransform;

    // Update is called once per frame
    void Update()
    {
        Health health = GetComponent<Health>();
        rectTransform.localScale = new Vector2(health.GetFraction(), 1);
    }
}
