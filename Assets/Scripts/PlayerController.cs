using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(isActive)
        {
            horizontal = Input.GetAxis("Horizontal");
            Movement();
            Flip();
            Jump();
        }
    }

    private void Movement()
    {
        if (horizontal != 0)
        {
            player.velocity = new Vector2(horizontal * speed, player.velocity.y);
            animator.SetInteger("run", 1);
        }
        animator.SetInteger("run", 0);
    }

    private void Jump()
    {
        if(isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                player.velocity = new Vector2(horizontal * speed, jumpForce);
                animator.SetTrigger("jump");
            }
        }
        else if(player.velocity.y < 0)
        {
            player.gravityScale = 10;
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

   /* private void Attack()
    {
        if(Input.GetButtonDown("Attack"))
        {
            
        }
    }*/

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
        if (collision.gameObject.tag == "DeathZone" || collision.gameObject.tag == "Death")
        {
            animator.SetBool("dead", true);
            manager.Dead();
        }
        else animator.SetBool("dead", false);
    }

    
}
