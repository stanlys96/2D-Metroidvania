using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    private Animator animator;
    private bool exploded = false;
    private float moveSpeed = 100f;
    private float moveDuration = 3f;
    public float lastMove = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        lastMove += Time.deltaTime;
    }

    public void TriggerBomb(GameObject player)
    {
        if (!exploded)
        {
            exploded = true;
            animator.SetTrigger("explode");
        }
    }
}
