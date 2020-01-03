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
    private bool isFacingRight = true;
    private bool isGrounded = true;
    [SerializeField] private GameManager manager;
    public bool isActive = false;


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        print(horizontal);
        if(isActive)
        {
          
            horizontal = CrossPlatformInputManager.GetAxis("Horizontal")*speed;
            Movement();
            Flip();
            Jump();
            Attack();
            Slide();
        }
    }

    public void Movement()
    {
        if (horizontal != 0 )
        {
            player.velocity = new Vector2(horizontal, player.velocity.y);

            if (isGrounded )
            {
                animator.SetInteger("run", 1);
            }
        }
        else if ( horizontal==0)
        {
            player.velocity = new Vector2(0, player.velocity.y);
            animator.SetInteger("run", 0);
        }
    }

    public void Jump()
    {
        if(isGrounded)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                player.velocity = new Vector2(horizontal * speed, jumpForce);
                animator.SetTrigger("jump");
            }
        }
        else if(player.velocity.y < 0)
        {
            player.gravityScale = 5;
        }
        else player.gravityScale = 1;
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
        if(CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            animator.SetTrigger("attack");
        }
    }

    public void Slide()
    {
        if(CrossPlatformInputManager.GetButtonDown("Down"))
        {
            if(isGrounded)
            {
                animator.SetTrigger("slide");
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            animator.SetBool("dead", true);
            manager.Dead();
        }
        else animator.SetBool("dead", false);
    }

    
}
