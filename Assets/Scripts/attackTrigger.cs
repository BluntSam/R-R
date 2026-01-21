using UnityEngine;

public class attackTrigger : MonoBehaviour
{
    public GameObject Hopps;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        //flip trigger location based on direction
        if (Hopps.GetComponent<playerMovement>().moveVector.x < 0)
        {
            transform.position = new Vector2(Hopps.transform.position.x - 2f, Hopps.transform.position.y + 2);
        }
        if (Hopps.GetComponent<playerMovement>().moveVector.x > 0)
        {
            transform.position = new Vector2(Hopps.transform.position.x + 2f, Hopps.transform.position.y + 2);
        }
    }
}
