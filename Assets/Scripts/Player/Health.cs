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
    private SpriteRenderer sRend;

    void Awake()
    {
        health = maxHealth;
        pMovement = GetComponent<playerMovement>();
        rBody = GetComponent<Rigidbody2D>();
        hoppsLocation = transform.position;
        sRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamageHopps(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            SceneManager.LoadSceneAsync("mainScene"); //Change to allow for checkpoints and loading into other levels later
        }

        StartCoroutine(DamageFlash());
    }

    public void GainHealth(int amount)
    {
        health += amount;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    private IEnumerator DamageFlash()
    {
        sRend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sRend.material.color = Color.white;
    }
}