using System.Collections;
using UnityEngine;

public class attackTrigger : MonoBehaviour
{
    //Components
    [SerializeField] private SpriteRenderer sRend;
    private Enemy enemy;
    [SerializeField] private GameObject Hopps;
    public normalAttack nAttack;

    //Variables
    public const string ENEMY_TAG = "Enemy";
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
                if (enemy == null)
                {
                    enemy = collision.gameObject.GetComponent<Enemy>();
                }

                Debug.Log("Enemy was Hit");
                enemy.TakeDamage(damage);

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
