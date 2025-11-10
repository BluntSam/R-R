using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;

    // Start is called once before the first execution of Update after the MonoBehavior is created
    void Start()
    {
        health = maxHealth;
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);

        }

    }

}
