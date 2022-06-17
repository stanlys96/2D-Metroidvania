using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDisplay : MonoBehaviour
{
    public RectTransform rectTransform;
    private Health playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localScale = new Vector2(playerHealth.GetFraction(), 1f);
    }
}
