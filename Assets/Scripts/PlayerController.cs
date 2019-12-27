using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator animator;
    private float horizontal;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 5f;
    private bool isFacingRight = true;
    private bool isGrounded = true;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        Movement();
        Flip();
        Jump();
    }

    private void Movement()
    {
        if (horizontal != 0)
        {
            player.velocity = new Vector2(horizontal * speed, player.velocity.y);
        }
    }

    private void Jump()
    {
        if(isGrounded)
        {
            if(Input.GetButton("Jump"))
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
