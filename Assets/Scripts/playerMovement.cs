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
    //X position
    private float xPosLastFrame;

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
        //  clampedMovement();
        //flipCharacterX();
    }

    //private void flipCharacterX()
    //{
    //    float input = Input.GetAxis("Horizontal");
    //    if (input > 0 && (transform.position.x > xPosLastFrame))
    //    {
    //        //If moving right
    //        spriteRenderer.flipX = false;
    //    }
    //    else if (input < 0 && (transform.position.x < xPosLastFrame))
    //    {
    //        //If moving left
    //        spriteRenderer.flipX = true;
    //    }

    //    xPosLastFrame = transform.position.x;
    //}
    private void clampedMovement()
    {
        //Stops player from escaping
        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x + playerHalfWidth, screenBounds.x - playerHalfWidth);
        Vector2 pos = transform.position; //Get the player's current position
        pos.x = clampedX; //Reassign x value to clamped pos
        transform.position = pos; //Reassign clamped value back to player
    }
    private void handleMovement()
    {
        //The actual movement
        float input = Input.GetAxis("Horizontal"); //Getting the horizontal axis
        movement.x = input * speed * Time.deltaTime; //Horizontal movement
        transform.Translate(movement); //Making them move
    }
}
