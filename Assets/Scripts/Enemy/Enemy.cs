using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))] //Also include any type of 2d Collider, but that is more flexible. (Not Triggers)
public class Enemy : MonoBehaviour
{
    //Private Fields

    private int maxHealth;      //Maximum Health of the Enemy
    private int currentHealth;  //Current Health of the Enemy

    private int attackPower;    //Attack Power of the Enemy

    private float moveSpeed;    //Movement Speed of the Enemy
    private float jumpForce;    //Jump Force of the Enemy

    private float sightRange;   //Targeting Range of the Enemy

    //Public/Protected Properties

    /// <summary>
    /// Maximum Enemy Health (Max HP)
    /// Protected Get-Set: Only Children of Enemy can get or set the Enemy's Max Health
    /// </summary>
    protected int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    /// <summary>
    /// Current Enemy Health (HP)
    /// Protected Get-Set: Only Children of Enemy can get or set the Enemy's Current Health
    /// </summary>
    protected int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    /// <summary>
    /// Enemy Attack Power (Attack Strength)
    /// Protected Get-Set: Only Children of Enemy can get or set the Enemy's Attack Power
    /// </summary>
    protected int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }

    /// <summary>
    /// Enemy Movement Speed (Speed)
    /// Public Get-Set: All can get or set the Enemy's Movement Speed
    /// </summary>
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    /// <summary>
    /// Enemy Jump Force (Jump)
    /// Public Get-Set: All can get or set the Enemy's Jump Force
    /// </summary>
    public float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }

    /// <summary>
    /// Enemy Sight Range (FOV Range)
    /// Public Get-Set: All can get or set the Enemy's Sight Range
    /// </summary>
    public float SightRange
    {
        get { return sightRange; }
        set { sightRange = value; }
    }
}

//welcome to hell, boys (and girl)
//Notes pasted from a google doc
//putting a get set for like everything cus idk what the fuck we are gonna do with this shit
/*
 * Enemy AI
Health
Take Damage
Behavior
Patrol
Flip
Chase
Attack
Damage

(health)
maxHealth
currentHealth

(attack)
damagePower (changed to Attack, is more clear)

(general movement)
moveSpeed
jumpForce

(specific movement)
chaseRange

(animation) <- AAAAAAAAAAAAAAAAHHHH
flip
walk
 */