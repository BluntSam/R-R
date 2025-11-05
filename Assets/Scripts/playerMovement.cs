using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //Declaring Variables
    //Speed of the character
    [SerializeField] private float speed = 5f;
    //Just the sprite renderer
    [SerializeField] private SpriteRenderer spriteRenderer;
    //Movement for 2D Vector
    private Vector2 movement;
    //Screen boundaries
    private Vector2 screenBounds;
    //Half the player's width
    private float playerHalfWidth;

    //Start Function: Called once before Update Function
    private void Start()
    {
        //Calculation to convert screen units into world units
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        //Getting the edge of the sprite
        playerHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    //Update Function: Called once per frame
    void Update()
    {
        handleMovement();
    }

    private void FixedUpdate()
    {
        GetComponent<Animator>().SetBool("isWalking", movement.x != 0);
    }

    private void handleMovement()
    {
        //The actual movement
        float input = Input.GetAxis("Horizontal"); //Getting the horizontal axis
        movement.x = input * speed * Time.deltaTime; //Horizontal movement
        transform.Translate(movement); //Making them move

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
