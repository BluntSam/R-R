using TMPro;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputManager))]
public class playerMovement : MonoBehaviour
{
    //Components
    private Rigidbody2D rBody;
    private SpriteRenderer sRend;
    private Animator pAnimator;
    private PlayerInputManager pInput;
    private Health hp;
    [SerializeField] private DeathScreen deathScreen;

    [Header("Movement Variables")]
    [SerializeField] private float speed = 10f; //Player Speed
    private bool isGrounded;
    private float horizontalVelocity;
    public Vector2 moveVector;
  
    [Header("Jump Variables")]
    public float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.5f;

    [Header("Other")]
    public int playerScore;
    private bool glideCheck = false;

    [Header("Sounds")]
    [SerializeField] private AudioSource soundEffects;
    [SerializeField] private AudioClip walkSounds;
    [SerializeField] private AudioClip jumpSounds;
    private bool walkCheck = false;

    //Start Function: Called once before Update Function
    private void Awake()
    {
        //Initializing
        rBody = GetComponent<Rigidbody2D>();
        sRend = GetComponent<SpriteRenderer>();
        pAnimator = GetComponent<Animator>();
        pInput = GetComponent<PlayerInputManager>();
        hp = GetComponent<Health>();
        soundEffects = GetComponent<AudioSource>();
    }

    //Update Function: Called once per frame
    private void FixedUpdate()
    {
        //Movement
        HandleMovement();
        HandleJump();
        HandleGlide();
    }

    private void Update()
    {
        //Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //Animation
        AnimatePlayer();
        FlipSprite();

        //Score
        GetComponentInChildren<TextMeshProUGUI>().text = $"Score: {playerScore}";
    }

    private void HandleMovement()
    {
        horizontalVelocity = pInput.MoveInput.x * speed;
        moveVector = new Vector2(horizontalVelocity, rBody.linearVelocity.y);
        rBody.linearVelocity = moveVector;
        
        if (Mathf.Abs(rBody.linearVelocity.x) > 0.1 && isGrounded && !soundEffects.isPlaying && !walkCheck)
        {
            StartCoroutine(walkingSoundPlay());
        }
        else
        {
            StopCoroutine(walkingSoundPlay());
        }
    }
    
    private IEnumerator walkingSoundPlay()
    {
        walkCheck = true;
        soundEffects.clip = walkSounds;
        soundEffects.Play();
        yield return new WaitForSeconds(1);
        walkCheck = false;
    }

    private void HandleJump()
    {
        if (pInput.JumpTriggered && isGrounded && !deathScreen.isDead)
        {
            rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, 0);
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            soundEffects.clip = jumpSounds;
            soundEffects.Play();
        }
    }
   
    private void HandleGlide()
    {
        if (pInput.GlideTriggered && isGrounded == false && rBody.linearVelocity.y <= -1f && !deathScreen.isDead)
        {
            rBody.gravityScale = -0.5f;
            Debug.Log("gliding");
            pAnimator.SetBool("isGliding", true);
            if (!glideCheck)
            {
                StartCoroutine(GlideHurt());
            }
        }
        else
        {
            rBody.gravityScale = 2;
            pAnimator.SetBool("isGliding", false);
            StopCoroutine(GlideHurt());
            StopCoroutine(GlideHurt());
        }
    }    
   
    private IEnumerator GlideHurt()
    {
        glideCheck = true;
        hp.TakeDamageHopps(1);
        yield return new WaitForSeconds(7f);
        glideCheck = false;
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
        if (rBody.linearVelocity.y <= -0.5f && !isGrounded && !pAnimator.GetBool("isGliding"))
        {
            pAnimator.SetBool("isFalling", true);
        }
        else
        {
            pAnimator.SetBool("isFalling", false);
        }
    }

    private void FlipSprite()
    {
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
