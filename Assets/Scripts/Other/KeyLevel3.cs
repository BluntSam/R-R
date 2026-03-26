using UnityEngine;

public class KeyLevel3 : MonoBehaviour
{
    [Header("Chest")]
    [SerializeField] private GameObject closedChestSprite;
    [SerializeField] private GameObject openChestSprite;

    private void Awake()
    {
        //Set chest closed, not open
        closedChestSprite.SetActive(true);
        openChestSprite.SetActive(false);
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
        //Set chest open, not closed
        closedChestSprite.SetActive(false);
        openChestSprite.SetActive(true);

        //Remove key
        gameObject.SetActive(false);
    }
}
