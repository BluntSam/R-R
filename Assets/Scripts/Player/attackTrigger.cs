using System.Collections;
using UnityEngine;

public class attackTrigger : MonoBehaviour
{
    //Components
    [SerializeField] private SpriteRenderer spriteRenderer;
    private enemyHealth enemyHealth;
    [SerializeField] private GameObject Hopps;
    public normalAttack nAttack;

    //Variables
    public const string ENEMY_TAG = "enemy";
    public int damage = 1;
    private bool isHitting;
    public bool canHitCooldown = false;

    void FixedUpdate()
    {
        Flipping();
    }

    private void Flipping()
    {
        //flip trigger location based on direction
        if (Hopps.GetComponent<playerMovement>().moveVector.x < 0)
        {
            transform.position = new Vector2(Hopps.transform.position.x - 2f, Hopps.transform.position.y + 2);
        }
        if (Hopps.GetComponent<playerMovement>().moveVector.x > 0)
        {
            transform.position = new Vector2(Hopps.transform.position.x + 2f, Hopps.transform.position.y + 2);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isHitting = nAttack.violent;
        if (isHitting)
        {
            if (collision.gameObject.tag == ENEMY_TAG && !canHitCooldown)
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
        canHitCooldown = true;
        yield return new WaitForSeconds(1f);
        canHitCooldown = false;
    }
}
