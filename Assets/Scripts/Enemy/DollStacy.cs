using UnityEngine;
using System.Collections;

public class DollStacy : Enemy
{
    //Components

    //[Header("Components")]
    private Rigidbody2D rBody;
    private SpriteRenderer sRend;
    private Animator eAnimate;
    private CapsuleCollider2D cCollider;
    public attackTriggerStacy aTrigger;

    //Hopps Components
    [Header("Hopps")]
    [SerializeField] private GameObject Hopps;
    private const string PLAYER_TAG = "Player";
    private Health playerHealth;

    //Field Variables

    //Mainly Initial Stats that Shouldn't change much
    private int mHealth = 3;       //Max Health
    private int cHealth = 3;       //Current Health
    private int atkPower = 1;      //Attack Power
    private float mSpeed = 1.5f;   //Movement Speed
    private float jForce = 2.0f;   //Jump Force
    private float sRange = 10.0f;  //Sight Range

    //Movement Variables
    [Header("Variables")]
    public Vector2 moveVector;
    public float distanceBetween;
    private Vector2 target;
    private bool harmfulToTouch = true;
    private bool canAttack = true;

    //Other Variables

    private void Awake()
    {
        //Initialization
        //Components
        rBody = GetComponent<Rigidbody2D>();
        sRend = GetComponent<SpriteRenderer>();
        eAnimate = GetComponent<Animator>();
        cCollider = GetComponent<CapsuleCollider2D>();
        aTrigger = GetComponentInChildren<attackTriggerStacy>();

        Hopps = GameObject.FindWithTag(PLAYER_TAG); //I need the whole bunny and I *WILL* take the whole bunny
        playerHealth = Hopps.GetComponent<Health>();

        //Setting Variables
        MaxHealth = mHealth;
        CurrentHealth = cHealth;
        AttackPower = atkPower;
        MoveSpeed = mSpeed;
        JumpForce = jForce;
        SightRange = sRange;

        //Movement Variable
        moveVector = new Vector2(mSpeed, 0);
    }

    private void Update()
    {
        //Animate();
        SpriteFlip();

        CheckForHopps();
        BehaviorChoice();

        //Either I update them only when needed, or just do it all the time. I'm tired. This is temporary.
        mHealth = MaxHealth;
        cHealth = CurrentHealth;
        jForce = JumpForce;
    }

    private void FixedUpdate()
    {
        
    }

    //Functions

    private void CheckForHopps()
    {
        distanceBetween = Vector2.Distance(transform.position, Hopps.transform.position); //Get distance between this Stacy and Hopps
    }

    private void BehaviorChoice()
    {
        sRange = SightRange;
        mSpeed = MoveSpeed;

        if (distanceBetween >= sRange) //Hopps is out of sight
        {
            Patrol();
        }
        else if (distanceBetween < sRange && distanceBetween > 4) //Hopps is in sight
        {
            Chase();
        }
        else if (distanceBetween <= 4) //Hopps is RIGHT THERE
        {
            Attack();
        }
    }

    private void Patrol()
    {
        if (NoFloor() || OnBlocked()) //If there's no floor, or there's something blocking this Stacy
        {
            StacyFlip();
        }
        else //There is floor, and there's nothing blocking this Stacy
        {
            transform.Translate(new Vector3((moveVector.x * mSpeed) * Time.deltaTime, 0, 0));
        }
    }

    private void StacyFlip()
    {
        moveVector.x = moveVector.x * -1;
    }

    private void Chase()
    {
        target = Hopps.transform.position - transform.position;
        if (target.x <= -1.5) { moveVector.x = -1.5f; }
        if (target.x >= 1.5) { moveVector.x = 1.5f; }
        transform.Translate(new Vector3((moveVector.x * mSpeed) * Time.deltaTime, 0, 0));
    }

    private void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            atkPower = AttackPower;
            aTrigger.isHitting = true;
            Chase(); //Keep running at Hopps
            StartCoroutine(HitCooldown());
        }
    }

    private IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }

    //Currently not needed as the only animation we are using, is Stacy constantly walking no matter what.
    //Will be needed when we decide to not do that.
    private void Animate()
    {
        //Where the animation code would go if we had any.
    }

    //Flips the Sprite *gasp*
    private void SpriteFlip()
    {
        if (moveVector.x < 0)
        {
            sRend.flipX = true;
        }
        if (moveVector.x > 0)
        {
            sRend.flipX = false;
        }
    }

    /// <summary>
    /// Uses Raycasting to observe if there's floor in front of the Stacy
    /// </summary>
    /// <returns></returns>
    public bool NoFloor()
    {
        Vector2 RCOrigin = (Vector2)transform.position + new Vector2(moveVector.x * 0.4f, -1.5f);
        Debug.DrawRay(RCOrigin, Vector2.down * 0.5f, Color.red);
        return !Physics2D.Raycast(RCOrigin, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
    }

    /// <summary>
    /// Uses Raycasting to observe if there's something blocking the Stacy's movement
    /// </summary>
    /// <returns></returns>
    public bool OnBlocked()
    {
        Vector2 RCOrigin = (Vector2)transform.position + new Vector2(moveVector.x, -1f);
        Vector2 RCDirection = new Vector2(moveVector.x, 0);
        Debug.DrawRay(RCOrigin, RCDirection * 0.5f, Color.red);
        return Physics2D.Raycast(RCOrigin, RCDirection, 0.5f, LayerMask.GetMask("Ground"));
    }

    //This just makes it hurt to touch the Stacy
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG && harmfulToTouch)
        {
            if (playerHealth == null)
            {
                playerHealth = collision.gameObject.GetComponent<Health>();
            }

            harmfulToTouch = false;
            atkPower = AttackPower;
            playerHealth.takeDamage(atkPower);
            Debug.Log($"Player Took {atkPower} Damage");
            StartCoroutine(HurtCooldown());
        }
    }

    private IEnumerator HurtCooldown()
    {
        yield return new WaitForSeconds(1f);
        harmfulToTouch = true;
    }
}

//welcome to hell, boys (and girl)
//Notes pasted from a google doc
//putting a get set for like everything cus idk what the fuck we are gonna do with this shit
/*
 * Enemy AI
Health - functional
Take Damage - functional
Die - functional
Patrol
Flip
Chase
Attack

(animation) <- AAAAAAAAAAAAAAAAHHHH
flip
walk

just reused my old checking for no floor and checking for blocked code
 */