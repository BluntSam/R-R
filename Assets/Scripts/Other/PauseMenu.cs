using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioSource bgMusicSource;
    public bool paused;
    public float cooldown;

    public void Awake() {
        paused = false;
        bgMusicSource.mute = false;
        Resume();
        cooldown = 0;
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        bgMusicSource.mute = true;
        paused = true;
        Time.timeScale = 0f;
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        bgMusicSource.mute = false;
        paused = false;
        Time.timeScale = 1f;

    }

    public void MainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menuScene");
    }
}
