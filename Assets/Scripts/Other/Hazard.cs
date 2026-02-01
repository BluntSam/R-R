using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Hazard : MonoBehaviour
{
    //wtf is this past me what were you doing
    //wtf^2 why were there separate scripts for spikes hitting player and enemy?
    public int damage = 10;
    private Health playerHealth;
    private Enemy enemy;
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
    public bool harmfulToPlayer = true;
    public bool harmfulToEnemy = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG && harmfulToPlayer)
        {
            if (playerHealth == null)
            {
                playerHealth = collision.gameObject.GetComponent<Health>();
            }
            
            playerHealth.TakeDamageStill(damage);
            Debug.Log($"Player Took {damage} Damage");
        }
        else if(collision.gameObject.tag == ENEMY_TAG && harmfulToEnemy)
        {
            if (enemy == null)
            {
                enemy = collision.gameObject.GetComponent<Enemy>();
            }

            Debug.Log($"{collision.gameObject.name} Took {damage} Damage");
            enemy.TakeDamage(damage);
        }
    }
}

