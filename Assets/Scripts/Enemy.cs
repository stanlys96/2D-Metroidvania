using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public ContactFilter2D filter;
    public GameObject patrolPoints;
    public GameObject hitFX;
    public float idleTime = 5f;
    public bool startLeft = false;
    public BombMover bombToInstantiate;
    public float speed = 20f;
    private CapsuleCollider2D capsuleCollider;
    private Collider2D[] hits = new Collider2D[10];
    private bool immune = false;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private int currentIndex = 0;
    private float timeSinceLastIdle = Mathf.Infinity;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (patrolPoints != null)
        {
            PatrolBehaviour();
        }
        timeSinceLastIdle += Time.deltaTime;
        capsuleCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            OnTriggerEnter2D(hits[i]);
            hits[i] = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitBox")
        {
            Instantiate(hitFX, transform.position, Quaternion.identity);
            GetComponent<Health>().TakeDamage(5f, GetComponent<Animator>());
        }
    }

    private void PatrolBehaviour()
    {
        Vector2 waypoint = patrolPoints.transform.GetChild(currentIndex).transform.position;
        if (gameObject.name == "Goblin")
        {
            print(rigidbody.velocity);
        }
        if (timeSinceLastIdle > idleTime)
        {
            if (Vector2.Distance(transform.position, waypoint) >= 1f)
            {
                animator.SetFloat("speed", 1);
                if (waypoint.x - patrolPoints.transform.position.x <= 0.1f)
                {
                    rigidbody.velocity = new Vector2(-speed, waypoint.y).normalized;
                    spriteRenderer.flipX = !startLeft;
                }
                else
                {
                    rigidbody.velocity = new Vector2(speed, waypoint.y).normalized;
                    spriteRenderer.flipX = startLeft;
                }
            }
            else
            {
                timeSinceLastIdle = 0;
                rigidbody.velocity = new Vector2(0, 0).normalized;
                animator.SetFloat("speed", 0);
                if (currentIndex == patrolPoints.transform.childCount - 1)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex++;
                }
            }
        }
    }

    public void InstantiateBomb()
    {
        BombMover bombMover = Instantiate(bombToInstantiate, transform.position, Quaternion.identity);
        bombMover.TriggerMove();
    }
}
