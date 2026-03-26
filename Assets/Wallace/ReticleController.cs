using UnityEngine;
using UnityEngine.InputSystem;

public class ReticleController : MonoBehaviour
{
    [Header("Settings")]
    public float reticleSpeed = 15f;
    public RectTransform reticleUI;

    private Vector2 moveInput;

    public void OnLook(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // If the stick is held in any direction, moveInput is NOT zero
        if (moveInput.sqrMagnitude > 0.01f)
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
        }
    }
}
