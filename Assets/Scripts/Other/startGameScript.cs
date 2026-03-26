using UnityEngine;
using UnityEngine.SceneManagement;
public class startGameScript : MonoBehaviour
{
   public void PlayGame ()
    {
        SceneManager.LoadSceneAsync("mainScene");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
