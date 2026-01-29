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
    //Components
    private Rigidbody2D rBody;
    private SpriteRenderer sRend;
    private Animator pAnimator;
    private PlayerInputManager pInput;

    [Header("Movement Variables")]
    [SerializeField] private float speed = 10f; //Player Speed
    private bool isGrounded;
    private float horizontalVelocity;
    public Vector2 moveVector;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 15f;
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
        HandleGlide();

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
        moveVector = new Vector2(horizontalVelocity, rBody.linearVelocity.y);
        rBody.linearVelocity = moveVector;
    }

    private void HandleJump()
    {
        if (pInput.JumpTriggered && isGrounded)
        {
            rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, 0);
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log(isGrounded);
            
        }
    }

    private void HandleGlide()
    {
        if (pInput.GlideTriggered && !isGrounded)
        {
            rBody.gravityScale = 0.5f;
            Debug.Log("gliding");
        }
        else
        {
            rBody.gravityScale = 2;
        }
    }    

    private void AnimatePlayer()
    {
        //Walking
        pAnimator.SetBool("isWalking", moveVector.x != 0);

        //Rising
        if (rBody.linearVelocity.y >= 0.5f && !isGrounded)
        {
            pAnimator.SetBool("isRising", true);
        }
        else
        {
            pAnimator.SetBool("isRising", false);
        }

        //Falling
        if (rBody.linearVelocity.y <= -0.5f && !isGrounded)
        {
            pAnimator.SetBool("isFalling", true);
        }
        else
        {
            pAnimator.SetBool("isFalling", false);
        }

        //flip sprite based on direction
        if (moveVector.x < 0)
        {
            sRend.flipX = true;
        }
        if (moveVector.x > 0)
        {
            sRend.flipX = false;
        }
    }
}
