using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector2(-3,-2);
            health = maxHealth;
            gameObject.SetActive(true);
        }
    }
}
