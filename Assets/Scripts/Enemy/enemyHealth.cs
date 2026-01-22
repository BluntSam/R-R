using UnityEngine;

[System.Obsolete]
public class enemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;

    void Awake()
    {
        health = maxHealth;
    }

    public void takeDamage(int amount) //Don't touch this Samuel. -Samuel
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

}
