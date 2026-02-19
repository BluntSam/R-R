using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputManager : MonoBehaviour
{
    //Yes, this is basically the code Wallace taught us. It was way better than ours.
    
    //Variables
    public Vector2 moveInput;
    private bool jumpPressed = false;
    private bool glidePressed = false;
    private bool isClicking = false;
    [SerializeField] PauseMenu pauseMenuCode;

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

    public bool IsClicking {
        get {
            return isClicking;
        }
        set {
            isClicking = value;
        }
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

    public void OnPause(InputAction.CallbackContext context)
    {
        pauseMenuCode.Pause();
    }

    public void OnStickClick(CallbackContext context) {
        if (context.performed) {
            Debug.Log("clicked");
            IsClicking = true;
        } else if (context.canceled) {
            IsClicking = false;
            Debug.Log("unclicked");
        }
    }

}
