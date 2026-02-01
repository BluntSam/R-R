using UnityEngine;
using System.Collections;

public class EggEd : Enemy
{
    //Components

    //[Header("Components")]
    private Rigidbody2D rBody;
    private SpriteRenderer sRend;
    private Animator eAnimate;
    private CapsuleCollider2D cCollider; //Normal Main Body Collider
    public attackTriggerEnemy aTrigger;
    private BoxCollider2D bCollider; //Top Collider

    //Hopps Components
    [Header("Hopps")]
    [SerializeField] private GameObject Hopps;
    private const string PLAYER_TAG = "Player";

    //Field Variables

    //Mainly Initial Stats that Shouldn't change much
    private int mHealth = 2;       //Max Health
    private int cHealth = 2;       //Current Health
    private int atkPower = 1;      //Attack Power
    private float mSpeed = 6.0f;   //Movement Speed
    private float jForce = 2.0f;   //Jump Force
    private float sRange = 10.0f;  //Sight Range

    //Movement Variables
    [Header("Variables")]
    public float distanceBetween;
    private Vector2 target;
    private bool canAttack = true;

    //The Egg Variables
    [SerializeField] private Sprite noTop;

    //Other Variables

    private void Awake()
    {
        //Initialization
        //Components
        rBody = GetComponent<Rigidbody2D>();
        sRend = GetComponent<SpriteRenderer>();
        eAnimate = GetComponent<Animator>();
        cCollider = GetComponent<CapsuleCollider2D>();
        aTrigger = GetComponentInChildren<attackTriggerEnemy>();
        bCollider = GetComponentInChildren<BoxCollider2D>();

        Hopps = GameObject.FindWithTag(PLAYER_TAG);

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
        EggCheck();

        //Animate();
        SpriteFlip();
    }

    private void FixedUpdate()
    {
        CheckForHopps();
        BehaviorChoice();

        mHealth = MaxHealth;
        cHealth = CurrentHealth;
        jForce = JumpForce;
    }

    //Functions

    private void EggCheck()
    {
        if (cHealth < 2)
        {
            sRend.sprite = noTop;
            cCollider.size = new Vector2(2f, 2f);
            cCollider.offset = new Vector2(-0.15f, -0.25f);
        }
    }

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
            EggFlip();
        }
        else //There is floor, and there's nothing blocking this Stacy
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG)
        {
            if (collision.otherCollider is BoxCollider2D)
            {
                //Debug.Log($"{gameObject.name} HIT HEAD");
                TakeDamage(10);
                Rigidbody2D hRBODY = Hopps.GetComponent<Rigidbody2D>();
                hRBODY.linearVelocity = new Vector2(hRBODY.linearVelocity.x, 0);
                hRBODY.AddForce(Vector2.up * Hopps.GetComponent<playerMovement>().jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}