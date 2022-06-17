using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HUD : MonoBehaviour
{
    public GameObject playerName;

    private void Start()
    {
        Sprite[] nameSprites = new Sprite[6];
        int index = 0;
        if (GameManager.instance != null && GameManager.instance.firstName != null)
        {
            foreach (char item in GameManager.instance.firstName)
            {
                foreach (GameManager.TextToSprite textToSprite in GameManager.instance.textToSprites)
                {
                    if (Char.ToLower(item) == textToSprite.text)
                    {
                        nameSprites[index] = textToSprite.textSprite;
                    }
                }
                index++;
            }
            for (int i = 0; i < playerName.transform.childCount; i++)
            {
                SpriteRenderer nameSpriteRenderer = playerName.transform.GetChild(i).transform.GetComponent<SpriteRenderer>();
                nameSpriteRenderer.sprite = nameSprites[i];
            }
        }
    }
}
