using UnityEngine;

public class damagedEnemy : MonoBehaviour
{
    public int damage = 1;
    private enemyHealth enemyHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (enemyHealth == null)
            {
                enemyHealth = collision.gameObject.GetComponent<enemyHealth>();
            }

            Debug.Log("Enemy Collided with Obstacle");
            enemyHealth.takeDamage(damage);
        }
    }
}