using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;


     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            pauseMenu.gameObject.SetActive(true);
        }
    }


    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        

       
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
       
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainMenu");
    }
}
