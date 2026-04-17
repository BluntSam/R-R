using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    float NavInput;

    public GameObject StartButton;
    public GameObject SettingsButton;
    public GameObject QuitButton;
    List<GameObject> buttonArray = new List<GameObject>();
    EventSystem eventSystem;
    int currentArrayPosition = 0;

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

    public void OnNavigate(CallbackContext context) {
        if(context.performed) {
            NavInput = context.ReadValue<Vector2>().y;
        } else if(context.canceled) {
            NavInput = 0;
        }
        if(NavInput > 0) {
            currentArrayPosition = Mathf.Clamp(currentArrayPosition + 1, 0, 2);
            eventSystem.SetSelectedGameObject(buttonArray[currentArrayPosition]);
        } else if(NavInput < 0) {
            currentArrayPosition = Mathf.Clamp(currentArrayPosition - 1, 0, 2);
            eventSystem.SetSelectedGameObject(buttonArray[currentArrayPosition]);
        }
        Debug.Log(currentArrayPosition);
    }

    private void Update() {
        currentArrayPosition = Mathf.Clamp(currentArrayPosition, 0, 2);
    }

    private void Awake() {
        eventSystem = GetComponent<EventSystem>();
        buttonArray[0] = StartButton;
        buttonArray[1] = SettingsButton;
        buttonArray[2] = QuitButton;
        eventSystem.firstSelectedGameObject = buttonArray[0];
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
