using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMover : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float moveSpeed = 0.05f;
    private float moveDuration = 3f;
    private float timeSinceLastMove = Mathf.Infinity;

    private void Update()
    {
        timeSinceLastMove += Time.deltaTime;
        Moving();
    }

    public void Moving()
    {
        if (timeSinceLastMove < moveDuration)
        {
            rigidbody.velocity = new Vector2(moveSpeed * rigidbody.velocity.x, rigidbody.velocity.y);
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    public void TriggerMove()
    {
        timeSinceLastMove = 0;
    }
}
