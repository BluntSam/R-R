using UnityEngine;

public class KeyLevel3 : MonoBehaviour
{
    [Header("Chest")]
    [SerializeField] private GameObject closedChestSprite;
    [SerializeField] private GameObject openChestSprite;
    [SerializeField] private GameObject LockedChestText; // Jam added this
    [SerializeField] private GameObject OneWayGate; // Jam added this

    private void Awake()
    {
        //Set chest closed, not open
        closedChestSprite.SetActive(true);
        openChestSprite.SetActive(false);

        //Jams text saying "we need key" is there
        LockedChestText.SetActive(true);

        //Jams one way gate to mini slide enemy section (prevents players from going wrong way)
        OneWayGate.SetActive(false);
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

        //Jams text that says the "chest is locked" is now gone
        LockedChestText.SetActive(false);

        //Jams OneWayGate is now ACTIVE
        OneWayGate.SetActive(true);

        //Remove key
        gameObject.SetActive(false);
    }
}
