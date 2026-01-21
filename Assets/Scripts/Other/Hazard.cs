using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Hazard : MonoBehaviour
{
    //wtf is this past me what were you doing
    //wtf^2 why were there separate scripts for spikes hitting player and enemy?
    public int damage = 10;
    private Health playerHealth;
    private enemyHealth enemyHealth;
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "enemy";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG)
        {
            if (playerHealth == null)
            {
                playerHealth = collision.gameObject.GetComponent<Health>();
            }
            
            playerHealth.takeDamage(damage);
            Debug.Log("Player Took Damage");
        }
        else if(collision.gameObject.tag == ENEMY_TAG)
        {
            if (enemyHealth == null)
            {
                enemyHealth = collision.gameObject.GetComponent<enemyHealth>();
            }

            Debug.Log("Enemy Took Damage");
            enemyHealth.takeDamage(damage);
        }
    }
}

