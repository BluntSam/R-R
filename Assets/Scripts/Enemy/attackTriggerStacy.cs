using UnityEngine;

public class attackTriggerStacy : MonoBehaviour
{
    public GameObject enemy;

    private void Awake()
    {
        //Initialize
        enemy = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        Flipping();
    }

    private void Flipping()
    {
        //flip trigger location based on direction
        if (enemy.GetComponent<DollStacy>().moveVector.x < 0)
        {
            transform.position = new Vector2(enemy.transform.position.x - 2f, enemy.transform.position.y);
        }
        if (enemy.GetComponent<DollStacy>().moveVector.x > 0)
        {
            transform.position = new Vector2(enemy.transform.position.x + 2f, enemy.transform.position.y);
        }
    }
}
