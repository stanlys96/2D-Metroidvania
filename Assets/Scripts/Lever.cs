using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isRight = true;
    public Sprite leftLever;
    public Sprite rightLever;

    private bool alreadyTriggered = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitBox" && !alreadyTriggered)
        {
            if (alreadyTriggered) return;
            if (isRight)
            {
                spriteRenderer.sprite = rightLever;
            }
            else
            {
                spriteRenderer.sprite = rightLever;
            }
            Door[] doors = GameObject.FindObjectsOfType<Door>();
            foreach (Door door in doors)
            {
                door.OpenDoor();
            }
        }
    }
}
