using UnityEngine;
using System.Collections;

public class FrogGuy : Enemy
{
    //Components
    //[Header("Components")]
    private Animator eAnimate;
    private CapsuleCollider2D cCollider;
    public attackTriggerEnemy aTrigger;

    //Hopps Components
    [Header("Hopps' Health")]
    private Health playerHealth;

    //Field Variables

    private int mHealth = 2;       //Max Health
    private int cHealth = 2;       //Current Health
    private int atkPower = 1;      //Attack Power
    private float mSpeed = 1f;     //Movement Speed
    private float jForce = 1f;     //Jump Force
    private float sRange = 10.0f;  //Sight Range

    //Movement Variables
    [Header("Variables")]
    public float distanceBetween;
    private Vector2 target;
    private bool canAttack = true;

    //Other Variables

    protected override void Awake()
    {
        base.Awake();

        //Initialization
        //Components
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
        distanceBetween = Vector2.Distance(transform.position, Hopps.transform.position); //Get distance between this enemy and Hopps
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
        else if (distanceBetween < sRange && distanceBetween > 3) //Hopps is in sight
        {
            Chase();
        }
        else if (distanceBetween <= 3) //Hopps is RIGHT THERE
        {
            Attack();
        }
    }

    private void Patrol()
    {
        if (NoFloor() || OnBlocked()) //If there's no floor, or there's something blocking this enemy
        {
            EnemyFlip();
        }
        else //There is floor, and there's nothing blocking this enemy
        {
            transform.Translate(new Vector3((moveVector.x * mSpeed) * Time.deltaTime, 0, 0));
        }
    }

    private void EnemyFlip()
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

    private void Animate()
    {
        //Where the animation code would go if we had any.
    }

    //Flips the Sprite *gasp*
    private void SpriteFlip()
    {
        if (moveVector.x < 0)
        {
            sRend.flipX = false;
        }
        if (moveVector.x > 0)
        {
            sRend.flipX = true;
        }
    }
}

//Jumping Enemy
//Instead of running at the player, it would jump at them.
//Wind up behavior that goes into a jump
//Possibly have it remember where Hopps was when it was going to jump so that you can dodge it?