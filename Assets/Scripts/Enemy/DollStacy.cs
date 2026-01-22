using UnityEngine;

public class DollStacy : Enemy
{
    //Components
    //[Header("Components")]
    private Rigidbody2D rBody;
    private SpriteRenderer sRend;
    private Animator eAnimate;
    private CapsuleCollider2D cCollider;
    

    //Field Variables
    //Mainly Initial Stats that Shouldn't change much
    private int mHealth = 3;       //Max Health
    private int cHealth = 3;       //Current Health
    private int atkPower = 1;      //Attack Power
    private float mSpeed = 2.0f;   //Movement Speed
    private float jForce = 2.0f;   //Jump Force
    private float sRange = 10.0f;  //Sight Range

    private void Awake()
    {
        //Initialization
        //Components
        rBody = GetComponent<Rigidbody2D>();
        sRend = GetComponent<SpriteRenderer>();
        eAnimate = GetComponent<Animator>();
        cCollider = GetComponent<CapsuleCollider2D>();
        //Setting Variables
        MaxHealth = mHealth;
        CurrentHealth = cHealth;
        AttackPower = atkPower;
        MoveSpeed = mSpeed;
        JumpForce = jForce;
        SightRange = sRange;
    }
}
