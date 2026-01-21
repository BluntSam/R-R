using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;

    void Awake()
    {
        health = maxHealth;
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            SceneManager.LoadSceneAsync("mainScene"); //Change to allow for checkpoints and loading into other levels later
        }
    }

    public void gainHealth(int amount)
    {
        health += amount;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }
}
