using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DebugControls : MonoBehaviour
{
    public void OnDebugLvl2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }

    public void OnDebugLvl3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadSceneAsync(3);
        }
    }
}
