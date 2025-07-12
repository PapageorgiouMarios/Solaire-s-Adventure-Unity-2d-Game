using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player_body;
    private BoxCollider2D player_collider;
    private SpriteRenderer player_sprite;
    private Animator player_animator;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 6f;

    private enum MovementState {idle, running, jumping, falling, attacking}
    // private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    private void Start()
    {
        player_body = GetComponent<Rigidbody2D>();
        player_collider = GetComponent<BoxCollider2D>();
        player_sprite = GetComponent<SpriteRenderer>();
        player_animator = GetComponent<Animator>();
        Debug.Log("PlayerMovement script started");
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        player_body.velocity = new Vector2(dirX * moveSpeed, player_body.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()) 
        {
            Debug.Log("Player Jump");
            player_body.velocity = new Vector2(player_body.velocity.x, jumpForce);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation() 
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            player_sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            player_sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (player_body.velocity.y > .1f) 
        {
            state = MovementState.jumping;
        }
        else if (player_body.velocity.y < -.1f) 
        {
            state = MovementState.falling;
        }
        player_animator.SetInteger("state", (int) state);
    }

    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
