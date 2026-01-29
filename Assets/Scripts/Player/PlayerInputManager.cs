using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    //Yes, this is basically the code Wallace taught us. It was way better than ours.
    
    //Variables
    private Vector2 moveInput;
    private bool jumpPressed = false;
    private bool glidePressed = false;

    //Properties
    public Vector2 MoveInput
    {
        get { return moveInput; }
    }
    public bool JumpTriggered
    {
        get { return jumpPressed; }
    }

    public bool GlideTriggered
    {
        get { return glidePressed; }
    }    

    //Functions
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpPressed = true;
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    public void OnGlide(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            glidePressed = true;
        }
        else if (context.canceled)
        {
            glidePressed = false;
        }
    }

}
