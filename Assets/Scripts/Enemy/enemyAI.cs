using System;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    //auuugh.wav

    //Components
    [SerializeField] private GameObject Hopps;
    [SerializeField] private SpriteRenderer sRend;
    //if i need to comment what these two are for you, my condolences

    //Variables
    public const string PLAYER_TAG = "Player"; //Hopps' Tag
    public float speed = 2f;        //Speed of Enemy
    public Vector2 moveVector;      //Movement Vector of Enemy
    public float distance;          //distance between Enemy and Hopps
    private Vector2 direction;      //Direction Enemy is facing
    private Vector2 target;         //Hopps' Location when in Chase Range
    public bool LOR;                //Left or Right
    public float chaseRange = 10f;  //Enemy's Range for spotting Hopps

    private void Awake()
    {
        //Initializing
        sRend = GetComponent<SpriteRenderer>();
        Hopps = GameObject.FindWithTag(PLAYER_TAG); //I need the whole bunny and I *WILL* take the whole bunny
    }

    private void Update()
    {
        LORCheck(); //Left or Right Check
        handleMovement(); //Movement?
        FlipSprite(); //Flip Sprite :)

        distance = Vector2.Distance(transform.position, Hopps.transform.position); //Getting the distance between Enemy and Hopps
    }

    //this is the problem.
    private void handleMovement()
    {
        if (distance >= chaseRange) //If Hopps is in Chase Range
        {
            if (noFloor() || blocked()) //If there is no floor or if there's something blocking the enemy
            {
                LOR = !LOR; //el flipo :)
            }
            else //If there's floor and no blocking objects
            {
                //idk what im doing man
                //idk what you were doing past me either
                transform.Translate(new Vector3(direction.x * speed * Time.deltaTime, 0, 0)); //Move forward based on Enemy speed
                
                moveVector.x = direction.x; //Sets direction vector to the same as the movement vector. yes, we did just use direction like the movement vector. no, the sprite won't flip without this.
            }
        }
        else
        {
            target = Hopps.transform.position - transform.position; //changing target vector towards Hopps
            transform.position = Vector2.MoveTowards(this.transform.position, Hopps.transform.position, speed * Time.deltaTime); //Moving towards target vector
            
            moveVector.x = target.x; //for sprite flip (again, won't flip visually without it. it probably shouldn't need this, but it does)

            if (moveVector.x < 0) //Change LOR to match Movement Vector
            {
                LOR = false;
            }
            if (moveVector.x > 0)
            {
                LOR = true;
            }
        }
    }

    /// <summary>
    /// This just checks if the enemy is facing left or right, and sets the direction vector to match.
    /// why did I code it this way? good question. I wish I knew.
    /// </summary>
    private void LORCheck()
    {
        //Setting Direction Vector to Left or Right
        if (LOR) //Right
        {
            direction.x = 1;
        }
        if (!LOR) //Left
        {
            direction.x = -1;
        }
    }

    /// <summary>
    /// Flips Enemy's Sprite based on Enemy's movement vector's facing direction
    /// </summary>
    private void FlipSprite()
    {
        //flip sprite based on movement direction
        if (moveVector.x < 0)
        {
            sRend.flipX = true;
        }
        if (moveVector.x > 0)
        {
            sRend.flipX = false;

        }
    }

    //it's all raycasting?
    //always has been
    //gunshot sound effect

    /// <summary>
    /// Uses Raycasting to observe if there's floor in front of the Enemy
    /// </summary>
    /// <returns></returns>
    public bool noFloor()
    {
        Vector2 RCOrigin = (Vector2)transform.position + new Vector2(direction.x * 0.4f, -1.5f);
        Debug.DrawRay(RCOrigin, Vector2.down * 0.5f, Color.red);
        return !Physics2D.Raycast(RCOrigin, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
    }

    /// <summary>
    /// Uses Raycasting to observe if there's something blocking the Enemy's movement
    /// </summary>
    /// <returns></returns>
    public bool blocked()
    {
        Vector2 RCOrigin = (Vector2)transform.position + new Vector2(direction.x, -1f);
        Vector2 RCDirection = new Vector2(direction.x, 0);
        Debug.DrawRay(RCOrigin, RCDirection * 0.5f, Color.red);
        return Physics2D.Raycast(RCOrigin, RCDirection, 0.5f, LayerMask.GetMask("Ground"));
    }
}