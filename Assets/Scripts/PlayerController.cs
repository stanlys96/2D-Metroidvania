using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    float speed = 10f;
    float pushingSpeed = 4f;
    public float jumpForce = 25f;
    public LayerMask whatIsGround;
    public Transform groundPoint;
    public float dashSpeed = 25f;
    public GameObject hitBoxRight;
    public GameObject hitBoxLeft;
    public float immuneTime = 2f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isOnGround;
    private bool isJumping = false;
    private bool isDoubleJumping = false;
    private bool canDoubleJump = false;
    private int index = 1;
    private float dashLength = 0.5f;
    private float dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;
    private bool onDash = false;
    private float activeMoveSpeed = 0f;
    private float attackCooldown = 0.5f;
    private float timeSinceLastAttack = Mathf.Infinity;
    private float timeSinceLastImmune = Mathf.Infinity;
    public SpriteRenderer afterImage;
    public float afterImageLifetime, timeBetweenAfterImages;
    private float afterImageCounter = Mathf.Infinity;
    private float flashDuration = 0.15f;
    private float flashCounter = 0f;
    private Vector2 hitBoxStartPosition;
    private bool isPushing = false;
    private Door door;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        activeMoveSpeed = speed;
        door = FindObjectOfType<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastImmune += Time.deltaTime;
        timeSinceLastAttack += Time.deltaTime;
        afterImageCounter -= Time.deltaTime;
        MoveCharacter();
        if (timeSinceLastImmune < immuneTime)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                flashCounter = flashDuration;
            }
        }
        else
        {
            spriteRenderer.enabled = true;
            flashCounter = 0;
        }
        if (timeSinceLastAttack >= attackCooldown)
        {
            hitBoxLeft.SetActive(false);
            hitBoxLeft.GetComponent<BoxCollider2D>().isTrigger = false;
            hitBoxLeft.GetComponent<BoxCollider2D>().enabled = false;
            hitBoxRight.SetActive(false);
            hitBoxRight.GetComponent<BoxCollider2D>().isTrigger = false;
            hitBoxRight.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void MoveCharacter()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        if (x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;  
        } 
        else if (x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        rigidbody.velocity = new Vector2(x * (isPushing ? pushingSpeed : activeMoveSpeed), rigidbody.velocity.y);

        if (Input.GetMouseButtonDown(1))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                onDash = true;
                rigidbody.velocity = new Vector2(100, rigidbody.velocity.y);
                if (animator != null)
                {
                    animator.SetFloat("jump", 0f);
                    animator.SetTrigger("dash");
                    afterImageCounter = 0;
                }
            }
        }

        if (afterImageCounter <= 0)
        {
            ShowAfterImage();
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = speed;
                dashCoolCounter = dashCooldown;
            }
            if (GetComponent<SpriteRenderer>().flipX)
            {
                rigidbody.velocity = new Vector2(-transform.localScale.x * activeMoveSpeed, rigidbody.velocity.y);
            } else
            {
                rigidbody.velocity = new Vector2(transform.localScale.x * activeMoveSpeed, rigidbody.velocity.y);
            }
        } 
        else
        {
            afterImageCounter = Mathf.Infinity;
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                isJumping = true;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                    canDoubleJump = false;
                    isDoubleJumping = true;
                }
            }
        }

        if (animator != null)
        {
            animator.SetFloat("speed", Mathf.Abs(x));

            if (isOnGround)
            {
                animator.SetFloat("jump", 0f);
            } 
            else
            {
                animator.SetFloat("speed", 0f);
                animator.SetFloat("jump", rigidbody.velocity.y);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (animator != null)
            {
                if (timeSinceLastAttack > attackCooldown)
                {
                    if (GetComponent<SpriteRenderer>().flipX)
                    {
                        hitBoxLeft.SetActive(true);
                        hitBoxLeft.GetComponent<BoxCollider2D>().enabled = true;
                        hitBoxLeft.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    else
                    {
                        hitBoxRight.SetActive(true);
                        hitBoxRight.GetComponent<BoxCollider2D>().enabled = true;
                        hitBoxRight.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    animator.SetFloat("jump", 0f);
                    animator.SetTrigger("attack");
                    timeSinceLastAttack = 0f;
                }
            }
        }
    }

    public void SetIsJumping(bool value)
    {
        isJumping = value;
    }

    public void SetCanDoubleJump(bool value)
    {
        canDoubleJump = value;
    }

    public void SetIsDoubleJumping(bool value)
    {
        isDoubleJumping = value;
        animator.SetBool("doubleJump", value);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (timeSinceLastImmune > immuneTime)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (collision.transform.GetComponent<Health>().GetCurrentHitpoint() > 0)
                {
                    timeSinceLastImmune = 0;
                    GetComponent<Health>().TakeDamage(5f, animator);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "haha")
        {
            print("??ASDJALKSDJ");
        }
        if (collision.gameObject.tag == "Stone" && !isPushing)
        {
            print("???");
            isPushing = true;
            animator.SetFloat("pushing", 1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stone")
        {
            isPushing = false;
            animator.SetFloat("pushing", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (timeSinceLastImmune > immuneTime)
        {
            if (collision.gameObject.tag == "Bomb" && gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Bomb>().TriggerBomb(gameObject);
                timeSinceLastImmune = 0;
                GetComponent<Health>().TakeDamage(20f, animator); 
            }
        }
    }

    public bool IsImmune()
    {
        return timeSinceLastImmune < immuneTime;
    }

    public void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = spriteRenderer.sprite;
        image.transform.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;

        Destroy(image.gameObject, afterImageLifetime);

        afterImageCounter = timeBetweenAfterImages;
    }

    public void StopMove()
    {
        rigidbody.velocity = Vector2.zero;
    }
}
