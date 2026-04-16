using UnityEngine;
using System.Collections;

public class DollStacy : Enemy
{
    //Components

    //[Header("Components")]
    private Animator eAnimate;
    private CapsuleCollider2D cCollider;
    public attackTriggerEnemy aTrigger;

    //Field Variables

    //Mainly Initial Stats that Shouldn't change much
    private int mHealth = 3;       //Max Health
    private int cHealth = 3;       //Current Health
    private int atkPower = 1;      //Attack Power
    private float mSpeed = 1.5f;   //Movement Speed
    private float jForce = 2.0f;   //Jump Force
    private float sRange = 10.0f;  //Sight Range
    private int score = 100;

    //Movement Variables
    [Header("Variables")]
    public float distanceBetween;
    private Vector2 target;
    private bool canAttack = true;

    //Animation Variables
    [Header("Animation Bools")]
    [SerializeField] private bool isWalkingAnim;
    [SerializeField] private bool isFallingAnim;
    [SerializeField] private bool isDeadAnim;
    [SerializeField] private bool isNoticeAnim;
    [SerializeField] private bool isSlashAnim;
    [SerializeField] private bool isNoSeeAnim;
    [SerializeField] private bool seenRecently;
    [SerializeField] private bool pauseMovement;

    protected override void Awake()
    {
        base.Awake();

        //Initialization
        //Components
        eAnimate = GetComponent<Animator>();
        cCollider = GetComponent<CapsuleCollider2D>();
        aTrigger = GetComponentInChildren<attackTriggerEnemy>();

        //Setting Variables
        MaxHealth = mHealth;
        CurrentHealth = cHealth;
        AttackPower = atkPower;
        MoveSpeed = mSpeed;
        JumpForce = jForce;
        SightRange = sRange;
        ScoreValue = score;

        //Movement Variable
        moveVector = new Vector2(mSpeed, 0);

        //walkin'
        isWalkingAnim = true;
        seenRecently = false;
        pauseMovement = false;
    }

    private void Update()
    {
        Animate();
        SpriteFlip();
        FallCheck();
    }

    private void FixedUpdate()
    {
        if (IsDead)
        {
            isDeadAnim = true;
            this.gameObject.layer = 3;
            return;
        }

        mHealth = MaxHealth;
        cHealth = CurrentHealth;
        jForce = JumpForce;

        if (!IsKnockedBack)
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
        if (IsKnockedBack) { return; }

        sRange = SightRange;

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
        if (seenRecently)
        {
            seenRecently = false;
            StartCoroutine(Lost());
        }
        else
        {
            if (NoFloor() || OnBlocked()) //If there's no floor, or there's something blocking this Stacy
            {
                StacyFlip();
            }
            else //There is floor, and there's nothing blocking this Stacy
            {
                if (!pauseMovement)
                {
                    transform.Translate(new Vector3((moveVector.x * mSpeed) * Time.deltaTime, 0, 0));
                }
            }
        }
    }

    private void StacyFlip()
    {
        moveVector.x = moveVector.x * -1;
    }

    private void Chase()
    {
        if (!seenRecently)
        {
            seenRecently = true;
            StartCoroutine(Noticed());
        }
        else
        {
            if (!pauseMovement)
            {
                target = Hopps.transform.position - transform.position;
                if (target.x <= -mSpeed) { moveVector.x = -mSpeed; }
                if (target.x >= mSpeed) { moveVector.x = mSpeed; }
                transform.Translate(new Vector3((moveVector.x * mSpeed) * Time.deltaTime, 0, 0));
            }
        }
    }

    private IEnumerator Noticed()
    {
        isWalkingAnim = false;
        pauseMovement = true;

        eAnimate.SetBool("IsNotice", true);

        yield return new WaitForSeconds(1f);

        eAnimate.SetBool("IsNotice", false);

        pauseMovement = false;
        isWalkingAnim = true;
    }

    private IEnumerator Lost()
    {
        isWalkingAnim = false;
        pauseMovement = true;


        eAnimate.SetBool("IsNoSee", true);

        yield return new WaitForSeconds(1f);

        eAnimate.SetBool("IsNoSee", false);

        pauseMovement = false;
        isWalkingAnim = true;
    }

    private void Attack()
    {
        if (canAttack)
        {
            isSlashAnim = true;
            isWalkingAnim = false;

            canAttack = false;
            atkPower = AttackPower;
            aTrigger.isHitting = true;
            Chase(); //Keep running at Hopps
            StartCoroutine(HitCooldown());
        }
    }

    private IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(0.75f);
        isSlashAnim = false;
        yield return new WaitForSeconds(1.25f);
        canAttack = true;
        isWalkingAnim = true;
    }

    private void FallCheck()
    {
        if (rBody.linearVelocity.y < -0.75f || rBody.linearVelocity.y > 0.75f)
        {
            eAnimate.SetBool("IsWalking", false);
            eAnimate.SetBool("IsFalling", true);
            eAnimate.SetBool("IsNotice", false);
            eAnimate.SetBool("IsSlash", false);
            eAnimate.SetBool("IsNoSee", false);
        }
        else
        {
            eAnimate.SetBool("IsFalling", false);
        }
    }

    //i think this is the worst thing ive ever coded
    //just hide it
    private void Animate()
    {
        //Stacy Walk (Stacy is Walking)
        if (isWalkingAnim)
        {
            eAnimate.SetBool("IsWalking", true);
        }
        else
        {
            eAnimate.SetBool("IsWalking", false);
        }

        //Stacy Dead (Stacy is Dead)
        if (isDeadAnim)
        {
            //Turn off everything that isn't dead
            eAnimate.SetBool("IsWalking", false);
            eAnimate.SetBool("IsFalling", false);
            eAnimate.SetBool("IsDead", true); //Dead Animation
            eAnimate.SetBool("IsNotice", false);
            eAnimate.SetBool("IsSlash", false);
            eAnimate.SetBool("IsNoSee", false);

            cCollider.size = new Vector2(1.4f, 1.8f);
        }
        //no else because you can't undie.
        //also it would be funny if you undied and then were stuck sliding in a dead pose

        //Stacy Slash (Stacy is Attacking Player)
        if (isSlashAnim)
        {
            eAnimate.SetBool("IsSlash", true);
            eAnimate.SetBool("IsWalking", false);
        }
        else
        {
            eAnimate.SetBool("IsSlash", false);
            eAnimate.SetBool("IsWalking", true);
        }
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

    //this is just to stop those stacys that run into each other in level 2 tbh
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StacyFlip();
        }
    }
}