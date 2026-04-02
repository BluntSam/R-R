using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    [Header("fill this with slop in the levels, only matters for main menu")]
    public GameObject StartButton;
    public GameObject SettingsButton;
    public GameObject QuitButton;

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
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0)) {
                //Cursor.
            }
            IsClicking = true;
        } else if (context.canceled) {
            IsClicking = false;
            Debug.Log("unclicked");
        }
    }

}
