using UnityEngine;

public class MainMenuCredits : MonoBehaviour
{
    //COPIED CODE FOR OPTIONS MENU FROM TALON
    // this is jam btw <(hi guys! ^w^)
    //were STILL reusing code from a previous menu its just easier this way ;v;

    [SerializeField] GameObject creditsMenu;

    public bool paused;
    public float cooldown;

    public void Credits()
    {
        creditsMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
    }

    public void BackCredits()
    {
        creditsMenu.SetActive(false);
        paused = true;
        Time.timeScale = 1f;

    }

}
