using System;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public GameObject Hopps;
    public float speed;
    public Vector2 movement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float distance;
    private Vector2 direction;

    void Update()
    {
        handleMovement();
    }

    private void handleMovement()
    {
        direction = -Vector2.one;
        movement.x = direction.x * speed * Time.deltaTime; //Horizontal movement
        transform.Translate(movement); //Making them move

        distance = Vector2.Distance(transform.position, Hopps.transform.position); //distance between enemy and Hopps
        direction = Hopps.transform.position - transform.position; //changing vector direction towards Hopps
        transform.position = Vector2.MoveTowards(this.transform.position, Hopps.transform.position, speed * Time.deltaTime); //Moving towards Hopps

        //flip sprite based on direction
        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;

        }
    }
}