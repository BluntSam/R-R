using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Damage : MonoBehaviour
{

    public int damage = 1;
    private Health playerHealth;
  
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (playerHealth == null)
            {
                playerHealth = collision.gameObject.GetComponent<Health>();
            }

            playerHealth.takeDamage(damage);
        }
    }
}

