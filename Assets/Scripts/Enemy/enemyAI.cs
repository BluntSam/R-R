using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class enemyAI : MonoBehaviour
{
    //auuugh.wav

    //Components
    public GameObject Hopps;
    [SerializeField] private SpriteRenderer sRend;

    //Variables
    public float speed = 2f;        //Speed of Enemy
    public Vector2 moveVector;      //Movement Vector of Enemy
    public float distance;          //distance between Enemy and Hopps
    private Vector2 direction;      //Direction Enemy is facing
    private Vector2 target;         //Hopps' Location when in Chase Range
    public bool LOR;                //Left or Right
    public float chaseRange = 10f;  //Enemy's Range for spotting Hopps

    void Update()
    {
        if (LOR) //Right
        {
            direction.x = 1;
        }
        if (!LOR) //Left
        {
            direction.x = -1;
        }

        handleMovement();

        distance = Vector2.Distance(transform.position, Hopps.transform.position);
    }

    private void handleMovement()
    {
        if (distance >= chaseRange)
        {
            if (noFloor() || blocked())
            {
                LOR = !LOR;
            }
            else
            {
                //idk what im doing man
                //idk what you were doing past me either
                transform.Translate(new Vector3(direction.x * speed * Time.deltaTime, 0, 0));
                
                moveVector.x = direction.x; //for sprite flip
            }
        }
        else
        {
            target = Hopps.transform.position - transform.position; //changing vector direction towards Hopps
            transform.position = Vector2.MoveTowards(this.transform.position, Hopps.transform.position, speed * Time.deltaTime); //Moving towards Hopps
            
            moveVector.x = target.x; //for sprite flip

            if (moveVector.x < 0) //LOR flip
            {
                LOR = false;
            }
            if (moveVector.x > 0)
            {
                LOR = true;
            }
        }

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

    public bool noFloor()
    {
        Vector2 RCOrigin = (Vector2)transform.position + new Vector2(direction.x * 0.4f, -1.5f);
        Debug.DrawRay(RCOrigin, Vector2.down * 0.5f, Color.red);
        return !Physics2D.Raycast(RCOrigin, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
    }

    public bool blocked()
    {
        Vector2 RCOrigin = (Vector2)transform.position + new Vector2(direction.x, -1f);
        Vector2 RCDirection = new Vector2(direction.x, 0);
        Debug.DrawRay(RCOrigin, RCDirection * 0.5f, Color.red);
        return Physics2D.Raycast(RCOrigin, RCDirection, 0.5f, LayerMask.GetMask("Ground"));
    }
}