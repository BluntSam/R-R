using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Hazard : MonoBehaviour
{
    //wtf is this past me what were you doing
    public int damage = 10;
    private Health playerHealth;
    public const string PLAYER_TAG = "Player";

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
    }
}

