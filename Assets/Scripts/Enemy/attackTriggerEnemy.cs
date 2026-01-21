using UnityEngine;

public class attackTriggerEnemy : MonoBehaviour
{
    public GameObject Enemy;

    // Update is called once per frame
    void Update()
    {
        Flipping();
    }

    private void Flipping()
    {
        //flip trigger location based on direction
        if (Enemy.GetComponent<enemyAI>().moveVector.x < 0)
        {
            transform.position = new Vector2(Enemy.transform.position.x - 2f, Enemy.transform.position.y);
        }
        if (Enemy.GetComponent<enemyAI>().moveVector.x > 0)
        {
            transform.position = new Vector2(Enemy.transform.position.x + 2f, Enemy.transform.position.y);
        }
    }
}
