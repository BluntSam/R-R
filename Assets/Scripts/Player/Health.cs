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

    public void TakeDamageStill(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            SceneManager.LoadSceneAsync("mainScene"); //Change to allow for checkpoints and loading into other levels later
        }
    }

    public void TakeDamage(int amount, GameObject enemyAttacker)
    {
        health -= amount;

        if (health <= 0)
        {
            SceneManager.LoadSceneAsync("mainScene"); //Change to allow for checkpoints and loading into other levels later
        }
        else
        {
            hoppsLocation = transform.position;
            enemyLocation = enemyAttacker.transform.position;
            StartCoroutine(KnockBackCooldown());
        }
    }

    private IEnumerator KnockBackCooldown()
    {
        Debug.Log("KNOCKBACK");
        pMovement.isKnockedBack = true;

        rBody.linearVelocity = new Vector2(0, 0);

        if (hoppsLocation.x > enemyLocation.x)
        {
            rBody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
        }
        else if (hoppsLocation.x < enemyLocation.x)
        {
            rBody.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.2f);
        pMovement.isKnockedBack = false;
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