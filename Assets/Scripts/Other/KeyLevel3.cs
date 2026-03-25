using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyLevel3 : MonoBehaviour
{
    [SerializeField] private GameObject closedSprite;
    [SerializeField] private GameObject openSprite;

    private SpriteRenderer sRend;
    private bool Key = false;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            closedSprite.SetActive(false);
            openSprite.SetActive(true);
        }
    }
}
