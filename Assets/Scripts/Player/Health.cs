using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    private playerMovement pMovement;
    private Vector2 hoppsLocation;
    private Vector2 enemyLocation;
    private Rigidbody2D rBody;

    void Awake()
    {
        health = maxHealth;
        pMovement = GetComponent<playerMovement>();
        rBody = GetComponent<Rigidbody2D>();
        hoppsLocation = transform.position;
    }

    public void TakeDamageHopps(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            SceneManager.LoadSceneAsync("mainScene"); //Change to allow for checkpoints and loading into other levels later
        }
    }

    public void TakeDamageHopps(int amount, GameObject enemyAttacker)
    {
        health -= amount;

        if (health <= 0)
        {
            SceneManager.LoadSceneAsync("mainScene"); //Change to allow for checkpoints and loading into other levels later
        }
    }

    public void GainHealth(int amount)
    {
        health += amount;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }
}