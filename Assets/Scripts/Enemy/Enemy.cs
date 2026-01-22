using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))] //Also include any type of 2d Collider, but that is more flexible. (Not Triggers)
public class Enemy : MonoBehaviour
{
    //Private Fields

    [Header("Enemy Stats")]
    [SerializeField] private int maxHealth;      //Maximum Health of the Enemy
    [SerializeField] private int currentHealth;  //Current Health of the Enemy
    [SerializeField] private int attackPower;    //Attack Power of the Enemy
    [SerializeField] private float moveSpeed;    //Movement Speed of the Enemy
    [SerializeField] private float jumpForce;    //Jump Force of the Enemy
    [SerializeField] private float sightRange;   //Targeting Range of the Enemy
    [SerializeField] public bool isDead;

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

    /// <summary>
    /// Enemy Is Dead (Dead)
    /// Public Get: All can get the Enemy's Death Status
    /// </summary>
    public bool IsDead
    {
        get { return isDead; }
    }

    //Functions

    /// <summary>
    /// Enemy Takes Damage
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        if (IsDead) { return; }

        CurrentHealth -= amount;
        Debug.Log($"{gameObject.name}'s HP is now: {CurrentHealth}.");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Enemy Dies
    /// </summary>
    protected void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died.");
        gameObject.SetActive(false);
    }
}