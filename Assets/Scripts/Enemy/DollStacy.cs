using UnityEngine;
using System.Collections;

public class DollStacy : Enemy
{
    //Components

    //[Header("Components")]
    private SpriteRenderer sRend;
    private Animator eAnimate;
    private CapsuleCollider2D cCollider;
    public attackTriggerEnemy aTrigger;

    //Hopps Components
    [Header("Hopps' Health")]
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
    public float distanceBetween;
    private Vector2 target;
    private bool harmfulToTouch = true;
    private bool canAttack = true;

    //Other Variables

    protected override void Awake()
    {
        base.Awake();

        //Initialization
        //Components
        sRend = GetComponent<SpriteRenderer>();
        eAnimate = GetComponent<Animator>();
        cCollider = GetComponent<CapsuleCollider2D>();
        aTrigger = GetComponentInChildren<attackTriggerEnemy>();

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
    }

    private void FixedUpdate()
    {
        mHealth = MaxHealth;
        cHealth = CurrentHealth;
        jForce = JumpForce;

        if (!isKnockedBack)
        {
            CheckForHopps();
            BehaviorChoice();
        }
    }

    //Functions

    private void CheckForHopps()
    {
        distanceBetween = Vector2.Distance(transform.position, Hopps.transform.position); //Get distance between this Stacy and Hopps
    }

    private void BehaviorChoice()
    {
        if (isKnockedBack) { return; }

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
        if (target.x <= -mSpeed) { moveVector.x = -mSpeed; }
        if (target.x >= mSpeed) { moveVector.x = mSpeed; }
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
            playerHealth.TakeDamageHopps(atkPower);
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