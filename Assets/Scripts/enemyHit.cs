using System.Collections;
using UnityEngine;

public class enemyHit : MonoBehaviour
{
    public int damage = 1;
    private enemyHealth enemyHealth;
    public normalAttack normalAttack;
    private bool isHit;
    public bool canHit = true;

    private void Update()
    {
        isHit = normalAttack.violence;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isHit)
        {
            if (collision.gameObject.tag == "enemy" && canHit)
            {
                if (enemyHealth == null)
                {
                    enemyHealth = collision.gameObject.GetComponent<enemyHealth>();
                }

                Debug.Log("Enemy was Hit");
                enemyHealth.takeDamage(damage);

                StartCoroutine(attackCooldown());
            }
        }
    }

    private IEnumerator attackCooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(1f);
        canHit = true;
    }
}
