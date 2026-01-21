using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class enemyAI : MonoBehaviour
{
    public GameObject Hopps;
    public float speed = 2f;
    public Vector2 movement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public float distance;
    private Vector2 direction;
    private Vector2 target;
    public bool LOR;
    public float chaseRange = 10f;

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

        distance = Vector2.Distance(transform.position, Hopps.transform.position); //distance between enemy and Hopps
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
                Vector3 moving = new Vector3(direction.x * speed * Time.deltaTime, 0, 0); //idk what im doing man
                transform.Translate(moving); //Making them move
                
                movement.x = direction.x; //for sprite flip
            }
        }
        else
        {
            target = Hopps.transform.position - transform.position; //changing vector direction towards Hopps
            transform.position = Vector2.MoveTowards(this.transform.position, Hopps.transform.position, speed * Time.deltaTime); //Moving towards Hopps
            
            movement.x = target.x; //for sprite flip

            if (movement.x < 0) //LOR flip
            {
                LOR = false;
            }
            if (movement.x > 0)
            {
                LOR = true;
            }
        }

        //flip sprite based on movement direction
        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;

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