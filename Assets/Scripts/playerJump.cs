using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    //The collision
    [SerializeField] private Rigidbody2D rigidBody;
    //The jump value
    [SerializeField] private float jumpForce = 6;
    //Just the sprite renderer
    [SerializeField] private SpriteRenderer spriteRenderer;
    //Movement for 2D Vector
    private Vector2 movement;
    //If touching the floor
    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.25f, LayerMask.GetMask("Ground"));
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        //this is for animating falling and rising aka jumping
        movement.y = rigidBody.linearVelocity.y;
        GetComponent<Animator>().SetBool("isRising", movement.y > 0);
        GetComponent<Animator>().SetBool("isFalling", movement.y < 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.25f);
    }

    //makes them jump upwards
    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
