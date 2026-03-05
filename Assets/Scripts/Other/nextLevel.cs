using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    private Scene levelOne;
    private Scene levelTwo;
    private Scene levelThree;

    private void Awake()
    {
        levelOne = SceneManager.GetSceneByBuildIndex(1);
        levelTwo = SceneManager.GetSceneByBuildIndex(2);
        levelTwo = SceneManager.GetSceneByBuildIndex(3);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           if (SceneManager.GetActiveScene() == levelOne)
            {
                SceneManager.LoadSceneAsync("Level2");
            }
            if (SceneManager.GetActiveScene() != levelOne && SceneManager.GetActiveScene() != levelThree ) 
            {
                SceneManager.LoadSceneAsync("Level3");
            }
                
        }
    }

    


}


