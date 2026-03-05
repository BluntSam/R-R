using UnityEngine;
using System.Collections;

public class FrogGuy : Enemy
{
    //Components
    //[Header("Components")]
    private Animator fAnimate;
    private CapsuleCollider2D cCollider;
    public attackTriggerEnemy aTrigger;

    //Hopps Components
    [Header("Hopps' Health")]
    private Health playerHealth;

    //Field Variables

    private int mHealth = 2;       //Max Health
    private int cHealth = 2;       //Current Health
    private int atkPower = 1;      //Attack Power
    private float mSpeed = 1.25f;     //Movement Speed
    private float jForce = 10.0f;     //Jump Force
    private float sRange = 15.0f;  //Sight Range
    private int score = 200;

    //Movement Variables
    [Header("Variables")]
    public float distanceBetween;
    private Vector2 target;
    private bool canAttack = true;
    private bool canJump = true;

    //Other Variables

    protected override void Awake()
    {
        base.Awake();

        //Initialization
        //Components
        fAnimate = GetComponent<Animator>();
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
        ScoreValue = score;

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

        if (!IsKnockedBack)
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
        if (IsKnockedBack) { return; }

        sRange = SightRange;
        mSpeed = MoveSpeed;
        jForce = JumpForce;

        if (distanceBetween >= sRange) //Hopps is out of sight
        {
            Patrol();
        }
        else if (distanceBetween < sRange && distanceBetween > 3.5f) //Hopps is in sight
        {
            Jump(); //This would change to windup which would lead to jump after (but that's too animation related rn)
        }
        else if (distanceBetween <= 3.5f) //Hopps is RIGHT THERE
        {
            Attack();
        }
    }

    private void Patrol()
    {
        fAnimate.SetBool("stand", true);
        fAnimate.SetBool("charge", false);
        fAnimate.SetBool("leap", false);
        fAnimate.SetBool("land", false);

        //Debug.Log($"Floor: {NoFloor()} Blocked: {OnBlocked()}");
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

    private void Jump()
    {
        if (canJump) { StartCoroutine(JumpWait()); }
    }

    private IEnumerator JumpWait()
    {
        fAnimate.SetBool("stand", false);
        fAnimate.SetBool("charge", true);
        fAnimate.SetBool("leap", false);
        fAnimate.SetBool("land", false);
        yield return new WaitForSeconds(0.5f);

        if (!NoFloor() && canJump) //Only if there is floor beneath them and they are allowed to jump
        {
            target = Hopps.transform.position - transform.position; //targeting Hopps

            if (target.x <= -mSpeed) { moveVector.x = -mSpeed; } //flipping movement vector based on Hopps' location so the enemy faces Hopps
            if (target.x >= mSpeed) { moveVector.x = mSpeed; }

            fAnimate.SetBool("stand", false);
            fAnimate.SetBool("charge", false);
            fAnimate.SetBool("leap", true);
            fAnimate.SetBool("land", false);

            rBody.AddForce(new Vector2(moveVector.x * mSpeed * 400, jForce * 75), ForceMode2D.Impulse); //jumps at Hopps
            canJump = false;
        }
        yield return new WaitForSeconds(1f);
        transform.Translate(new Vector3(0, 0, 0));
        fAnimate.SetBool("stand", false);
        fAnimate.SetBool("charge", false);
        fAnimate.SetBool("leap", false);
        fAnimate.SetBool("land", true);
        yield return new WaitForSeconds(3f);
        fAnimate.SetBool("stand", true);
        fAnimate.SetBool("charge", false);
        fAnimate.SetBool("leap", false);
        fAnimate.SetBool("land", false);
        canJump = true;
    }

    private void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            atkPower = AttackPower;
            aTrigger.isHitting = true;
            transform.Translate(new Vector3((moveVector.x * mSpeed) * Time.deltaTime, 0, 0));
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

    //Makes the frog bouncy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider is CapsuleCollider2D) //only the capsule one
        {
            if (collision.gameObject.tag == PLAYER_TAG) //Making sure the colliding object is the player
            {
                Rigidbody2D hRBODY = Hopps.GetComponent<Rigidbody2D>();
                hRBODY.linearVelocity = new Vector2(hRBODY.linearVelocity.x, 0);
                hRBODY.AddForce(Vector2.up * Hopps.GetComponent<playerMovement>().jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}