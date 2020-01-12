using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator animator;
    private float horizontal;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpForce = 15f;
    private bool isFacingRight;
    private bool isGrounded;
    [SerializeField] private GameManager manager;
    public bool isActive;
    private bool isFalling;
    private bool isDown;
    private bool isInvisible;
    private SpriteRenderer renderer;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        isActive = false;
        isFacingRight = true;
        isGrounded = true;
        isFalling = false;
        isDown = false;
        isInvisible = false;
    }

    void FixedUpdate()
    {
        if(isActive)
        {
            //horizontal = Input.GetAxis("Horizontal") * speed;
            horizontal = CrossPlatformInputManager.GetAxis("Horizontal") * speed;
            Movement();
            Flip();
            Jump();
            Attack();
            CrowlMove();
        }
    }

    public void Movement()
    {
        if (horizontal != 0)
        {
            player.velocity = new Vector2(horizontal, player.velocity.y);

            if (isGrounded )
            {
                animator.SetInteger("run", 1);
            }
        }
        else if (horizontal == 0)
        {
            player.velocity = new Vector2(0, player.velocity.y);
            animator.SetInteger("run", 0);
        }
    }

    public void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (isGrounded && !isDown)
            {
                player.velocity = new Vector2(horizontal, jumpForce);
                animator.SetTrigger("jump");
            }
        }
        else if (player.velocity.y < 0)
        {
            animator.SetBool("falling", true);
            player.gravityScale = 8;
        }
        else
        {
            animator.SetBool("falling", false);
            player.gravityScale = 1;
        }
    }

    private void Flip()
    {
        if(horizontal < 0 && isFacingRight || horizontal > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void Attack()
    {
        if(horizontal == 0 && CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            animator.SetTrigger("attack");

            if(isInvisible)
            {
                renderer.color = new Color(1f, 1f, 1f, 1f);
                isInvisible = !isInvisible;
            }
        }
    }

    public void Crowl()
    {
        isDown = !isDown;
        if (isDown)
        {
            animator.SetBool("crowlStay", true);
        }
        else animator.SetBool("crowlStay", false);
    }

    public void CrowlMove()
    {
        if (isDown && horizontal != 0)
        {
            player.velocity = new Vector2(horizontal, player.velocity.y);
            animator.SetBool("crowlMove", true);
        }
        else animator.SetBool("crowlMove", false);
    }

    public void Invis()
    {
        isInvisible = !isInvisible;
        if (isInvisible)
        {
            renderer.color = new Color(1f, 1f, 1f, 0.25f);
        }
        else renderer.color = new Color(1f, 1f, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death" && !isDown)
        {
            animator.SetBool("dead", true);
            manager.Dead();
        }
        else if (collision.gameObject.tag == "Death" && isDown)
        {
            animator.SetBool("crawlDead", true);
            manager.Dead();
        }
        else
        {
            animator.SetBool("dead", false);
            animator.SetBool("crawlDead", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
