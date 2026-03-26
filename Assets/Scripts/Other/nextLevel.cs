using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class nextLevel : MonoBehaviour
{
    private Scene levelOne;
    private Scene levelTwo;
    private Scene levelThree;
    public VideoPlayer video;

    public startGameScript startGameScript;
    public GameObject videoScreen;

    private void Awake()
    {
        videoScreen.SetActive(false);
        levelOne = SceneManager.GetSceneByBuildIndex(1);
        levelTwo = SceneManager.GetSceneByBuildIndex(2);
        levelThree = SceneManager.GetSceneByBuildIndex(3);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            play();  
        }
    }
    public void play() {
        videoScreen.SetActive(true);
        video.Play();
        video.loopPointReached += Video_loopPointReached;
    }
    private void Video_loopPointReached(VideoPlayer source)
    {
        video.Stop();
        if (SceneManager.GetActiveScene() == levelOne)
        {
            SceneManager.LoadSceneAsync(2);
        }
        else if(SceneManager.GetActiveScene() == levelTwo)
        {
            SceneManager.LoadSceneAsync(3);
        }
        else if (SceneManager.GetActiveScene() == levelThree)
        {
            SceneManager.LoadSceneAsync(0);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            startGameScript.PlayGame();
        }
    }

}


