using UnityEngine;
using UnityEngine.Video;

public class CutscenePlayer : MonoBehaviour
{
    public VideoPlayer video;
    
    public startGameScript startGameScript;

    public GameObject videoScreen;
    private void Awake()
    {
        videoScreen.SetActive(false);
    }
    public void play()
    {
        videoScreen.SetActive(true);
        video.Play();
        video.loopPointReached += Video_loopPointReached;
    }

    private void Video_loopPointReached(VideoPlayer source)
    {
        video.Stop();
        startGameScript.PlayGame();
    }
}
