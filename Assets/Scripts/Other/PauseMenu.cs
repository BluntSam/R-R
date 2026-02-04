using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioSource audioSource;
    public bool paused;
    public float cooldown;

    public void Awake() {
        paused = false;
        audioSource.mute = false;
        Resume();
        cooldown = 0;
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        audioSource.mute = true;
        paused = true;
        Time.timeScale = 0f;
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        audioSource.mute = false;
        paused = false;
        Time.timeScale = 1f;

    }

    public void MainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menuScene");
    }
}
