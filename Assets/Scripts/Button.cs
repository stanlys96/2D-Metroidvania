using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public Sprite buttonDown;
    public Sprite buttonUp;
    public bool buttonTriggered = false;
    public UnityEvent onButtonDown;
    public UnityEvent onButtonUp;
    public Door door;

    private SpriteRenderer spriteRenderer;
    private EdgeCollider2D edgeCollider;
    private PolygonCollider2D polygonCollider;
    private bool playerOnTop = false;
    private bool stoneOnTop = false;
    private bool doorShouldGoUp = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (door.openDoorAnimationDone && doorShouldGoUp)
        {
            door.UpAgain();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Stone")
        {
            stoneOnTop = true;
        }

        if (collision.tag == "Player")
        {
            playerOnTop = true;
        }

        if (stoneOnTop || playerOnTop)
        {
            spriteRenderer.sprite = buttonDown;
            edgeCollider.enabled = false;
            polygonCollider.enabled = true;
            buttonTriggered = true;
            onButtonDown.Invoke();
            doorShouldGoUp = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Stone")
        {
            stoneOnTop = false;
        }

        if (collision.tag == "Player")
        {
            playerOnTop = false;
        }

        if (!playerOnTop && !stoneOnTop)
        {
            spriteRenderer.sprite = buttonUp;
            edgeCollider.enabled = true;
            polygonCollider.enabled = false;
            buttonTriggered = false;
            onButtonUp.Invoke();
            doorShouldGoUp = true;
        }
    }
}
