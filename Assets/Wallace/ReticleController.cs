using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReticleController : MonoBehaviour
{
    [Header("Settings")]
    public float reticleSpeed = 15f;
    public Transform reticleUI;
    string scheme;
    PlayerInput input;
    Mouse mouse;
    Vector2 mousePos;
    [SerializeField] GameObject player;

    private Vector2 moveInput;

    public void Awake() {
        input = player.GetComponent<PlayerInput>();
        scheme = input.defaultControlScheme;
        mouse = Mouse.current;
        mousePos = mouse.position.ReadValue();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        mousePos = mouse.position.ReadValue();
    }

    private void Update()
    {
        if(input.currentControlScheme != scheme) {
            scheme = input.currentControlScheme;
        }
        if (scheme == "Gamepad") {
            Cursor.visible = false;
        } else {
            Cursor.visible = true;
        }
        // If the stick is held in any direction, moveInput is NOT zero
        if (moveInput.sqrMagnitude > 0.01f && scheme == "Gamepad")
        {
            // Calculate movement
            Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * reticleSpeed * Time.deltaTime;

            // Apply to the reticle's position
            reticleUI.position += movement;

            // Clamp to screen so they don't lose the reticle during a fight
            Vector3 screenPos = reticleUI.position;
            screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
            screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);
            reticleUI.position = screenPos;
        } else if(scheme == "Keyboard&Mouse") {
            Vector3 screenPos = new Vector3(mousePos.x, mousePos.y, 0);
            screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
            screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);
            reticleUI.position = screenPos;
        }
    }
}
