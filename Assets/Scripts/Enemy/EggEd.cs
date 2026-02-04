using UnityEngine;
using System.Collections;

public class EggEd : Enemy
{
    //Components

    //[Header("Components")]
    private Animator eAnimate;
    private CapsuleCollider2D cCollider; //Normal Main Body Collider
    public attackTriggerEnemy aTrigger;
    private BoxCollider2D bCollider; //Top Collider

    //Field Variables

    //Hopps Components
    [Header("Hopps' Health")]
    private Health playerHealth;

    //Mainly Initial Stats that Shouldn't change much
    private int mHealth = 10;       //Max Health
    private int cHealth = 10;       //Current Health
    private int atkPower = 1;      //Attack Power
    private float mSpeed = 3.0f;   //Movement Speed
    private float jForce = 2.0f;   //Jump Force
    private float sRange = 10.0f;  //Sight Range

    //Movement Variables
    [Header("Variables")]
    public float distanceBetween;
    private Vector2 target;
    private bool canAttack = true;

    //The Egg Variables
    [SerializeField] private Sprite noTop;
    [SerializeField] private Sprite yesTop;
    [SerializeField] private int hpNoTopLevel = 9;
    [SerializeField] private bool hatless;
    private bool harmfulToTouch = true;

    //Other Variables

    protected override void Awake()
    {
        base.Awake();

        //Initialization
        //Components
        eAnimate = GetComponent<Animator>();
        cCollider = GetComponent<CapsuleCollider2D>();
        aTrigger = GetComponentInChildren<attackTriggerEnemy>();
        bCollider = GetComponentInChildren<BoxCollider2D>();

        //Setting Variables
        MaxHealth = mHealth;
        if (!hatless)
        {
            CurrentHealth = cHealth;
        }
        else if (hatless)
        {
            CurrentHealth = hpNoTopLevel;
        }
        AttackPower = atkPower;
        MoveSpeed = mSpeed;
        JumpForce = jForce;
        SightRange = sRange;

        //Movement Variable
        moveVector = new Vector2(mSpeed, 0);
    }

    private void Update()
    {
        EggCheck();

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

    private void EggCheck() //To see if the top should be removed
    {
        if (cHealth <= hpNoTopLevel || hatless)
        {
            hatless = true;
            sRend.sprite = noTop;
            cCollider.size = new Vector2(2f, 2f);
            cCollider.offset = new Vector2(-0.15f, -0.5f);
            SpriteFlip();
        }

        if (!hatless)
        {
            sRend.sprite = yesTop;
            cCollider.size = new Vector2(2f, 3f);
            cCollider.offset = new Vector2(-0.15f, 0f);
            SpriteFlip();
        }
    }

    private void CheckForHopps()
    {
        distanceBetween = Vector2.Distance(transform.position, Hopps.transform.position); //Get distance between this Ed and Hopps
    }

    private void BehaviorChoice()
    {
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
        if (NoFloor() || OnBlocked()) //If there's no floor, or there's something blocking this Ed
        {
            EggFlip();
        }
        else //There is floor, and there's nothing blocking this Ed
        {
            transform.Translate(new Vector3((moveVector.x * mSpeed) * Time.deltaTime, 0, 0));
        }
    }

    private void EggFlip()
    {
        moveVector.x = moveVector.x * -1;
    }

    private void Chase()
    {
        if (NoFloor()) //If there's no floor
        {
            target = Hopps.transform.position - transform.position;
            if (target.x <= -mSpeed) { moveVector.x = -mSpeed; }
            if (target.x >= mSpeed) { moveVector.x = mSpeed; }
            transform.Translate(new Vector3(0, 0, 0));
        }
        else //If there's floor
        {
            target = Hopps.transform.position - transform.position;
            if (target.x <= -mSpeed) { moveVector.x = -mSpeed; }
            if (target.x >= mSpeed) { moveVector.x = mSpeed; }
            transform.Translate(new Vector3((moveVector.x * mSpeed) * Time.deltaTime, 0, 0));
        }
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

    //Flips the Sprite *gasp* (also changes collider offsets as this sprite is off center)
    private void SpriteFlip()
    {
        if (moveVector.x < 0)
        {
            sRend.flipX = false;
            cCollider.offset = new Vector2(-0.15f, cCollider.offset.y);
            bCollider.offset = new Vector2(-0.15f, bCollider.offset.y);
        }
        if (moveVector.x > 0)
        {
            sRend.flipX = true;
            cCollider.offset = new Vector2(0.15f, cCollider.offset.y);
            bCollider.offset = new Vector2(0.15f, bCollider.offset.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //This is only in regards to the box collider for a head jump hit
    {
        if (collision.gameObject.tag == PLAYER_TAG) //checking if it is the player colliding with the enemy
        {
            if (collision.otherCollider is BoxCollider2D) //checking that it is the box collider and not the normal one
            {
                if (cHealth <= hpNoTopLevel) //checking to see if the top is off
                {
                    //Debug.Log($"{gameObject.name} HIT HEAD");
                    TakeDamage(20);
                    Rigidbody2D hRBODY = Hopps.GetComponent<Rigidbody2D>();
                    hRBODY.linearVelocity = new Vector2(hRBODY.linearVelocity.x, 0);
                    hRBODY.AddForce(Vector2.up * Hopps.GetComponent<playerMovement>().jumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider is CapsuleCollider2D) //Making sure it's not the box collider
        {
            if (collision.gameObject.tag == PLAYER_TAG && harmfulToTouch) //Making sure the colliding object is the player, and that the cooldown coroutine is not still active
            {
                if (playerHealth == null) //Checking that we have access to the player's health
                {
                    playerHealth = collision.gameObject.GetComponent<Health>(); //grabbing it if not
                }

                harmfulToTouch = false; //to make sure we don't hurt hopps fivty times over in like one nanosecond
                atkPower = AttackPower; //how strong the hit will be
                playerHealth.TakeDamageHopps(atkPower); //attacking hopps
                Debug.Log($"Player Took {atkPower} Damage"); //debug note about the damage
                StartCoroutine(HurtCooldown()); //cooldown
            }
        }
    }

    private IEnumerator HurtCooldown()
    {
        yield return new WaitForSeconds(1f); //wait 1 second
        harmfulToTouch = true; //now you can hit again
    }
}