using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyLevel3 : MonoBehaviour
{
    [SerializeField] private GameObject closedSprite;
    [SerializeField] private GameObject openSprite;

    private SpriteRenderer sRend;
    

    private void Awake()
    {
        sRend = GetComponent<SpriteRenderer>();
        key();
    }
    public void key()
    {
        closedSprite.SetActive(true);
        openSprite.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collect();
        }
    }

    public void Collect()
    {
        closedSprite.SetActive(false);
        openSprite.SetActive(true);
        gameObject.SetActive(false);
    }
}
