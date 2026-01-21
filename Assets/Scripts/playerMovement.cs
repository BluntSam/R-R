using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputManager))]
public class playerMovement : MonoBehaviour
{
    //this code was so ass

    //Components
    private Rigidbody2D rBody;
    private SpriteRenderer sRend;
    private Animator pAnimator;
    private PlayerInputManager pInput;

    [Header("Movement Variables")]
    [SerializeField] private float speed = 8f; //Player Speed
    private bool isGrounded;
    private float horizontalVelocity;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.5f;


    //Start Function: Called once before Update Function
    private void Awake()
    {
        //Initializing
        rBody = GetComponent<Rigidbody2D>();
        sRend = GetComponent<SpriteRenderer>();
        pAnimator = GetComponent<Animator>();
        pInput = GetComponent<PlayerInputManager>();
    }

    //Update Function: Called once per frame
    private void FixedUpdate()
    {
        //Movement
        HandleMovement();
        HandleJump();

        //Animation
        AnimatePlayer();
    }

    private void Update()
    {
        //Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void HandleMovement()
    {
        horizontalVelocity = pInput.MoveInput.x * speed;
        rBody.linearVelocity = new Vector2(horizontalVelocity, rBody.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (pInput.JumpTriggered && isGrounded)
        {
            rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, 0);
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void AnimatePlayer()
    {
        /*
        //Animation
        pAnimator.SetBool("isWalking", movement.x != 0);

        //flip sprite based on direction
        if (movement.x < 0)
        {
            sRend.flipX = true;
        }
        if (movement.x > 0)
        {
            sRend.flipX = false;
        }
        */
    }
}
