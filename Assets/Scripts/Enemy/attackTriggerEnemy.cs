using UnityEngine;

public class attackTriggerEnemy : MonoBehaviour
{
    public GameObject Enemy;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        //flip trigger location based on direction
        if (Enemy.GetComponent<enemyAI>().movement.x < 0)
        {
            transform.position = new Vector2(Enemy.transform.position.x - 2f, Enemy.transform.position.y);
        }
        if (Enemy.GetComponent<enemyAI>().movement.x > 0)
        {
            transform.position = new Vector2(Enemy.transform.position.x + 2f, Enemy.transform.position.y);
        }
    }
}
