using UnityEngine;
using UnityEngine.SceneManagement;
public class startGameScript : MonoBehaviour
{

    

   public void PlayGame ()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
