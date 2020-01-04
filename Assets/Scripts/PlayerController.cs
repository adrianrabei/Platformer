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


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isActive = false;
        isFacingRight = true;
        isGrounded = true;
        isFalling = false;
        isDown = false;

    }

    void FixedUpdate()
    {
        if(isActive)
        {
            horizontal = Input.GetAxis("Horizontal");
            //horizontal = CrossPlatformInputManager.GetAxis("Horizontal")*speed;
            Movement();
            Flip();
            Jump();
            Attack();
            CrouchMove();
        }
    }

    public void Movement()
    {
        if (horizontal != 0)
        {
            player.velocity = new Vector2(horizontal * speed, player.velocity.y);

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
        if(isGrounded && !isDown)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {      
                player.velocity = new Vector2(horizontal * speed, jumpForce);
                animator.SetTrigger("jump");
            }
        }
        else if(player.velocity.y < 0)
        {
            animator.SetBool("falling", true);
            player.gravityScale = 7.5f;
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
        }
    }

    public void Crouch()
    {
        isDown = !isDown;
        if (isDown)
        {
            animator.SetBool("crouchStay", true);
        }
        else animator.SetBool("crouchStay", false);
    }

    public void CrouchMove()
    {
        if (isDown && horizontal != 0)
        {
            player.velocity = new Vector2(horizontal * speed * 0.6f, player.velocity.y);
            animator.SetBool("crouchMove", true);
        }
        else animator.SetBool("crouchMove", false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            animator.SetBool("dead", true);
            manager.Dead();
        }
        else animator.SetBool("dead", false);
    }

    
}
