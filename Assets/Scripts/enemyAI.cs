using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public GameObject Hopps;
    public float speed;
    private Vector2 movement;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float distance;

    void Update()
    {
        handleMovement();
    }

    private void handleMovement()
    {
        distance = Vector2.Distance(transform.position, Hopps.transform.position); //distance between enemy and Hopps
        Vector2 direction = Hopps.transform.position - transform.position; //changing vector direction towards Hopps
        transform.position = Vector2.MoveTowards(this.transform.position, Hopps.transform.position, speed * Time.deltaTime); //Moving towards Hopps

        //flip sprite based on direction
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;

        }
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
