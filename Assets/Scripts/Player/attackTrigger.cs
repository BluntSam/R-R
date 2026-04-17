using System.Collections;
using UnityEngine;

public class attackTrigger : MonoBehaviour
{
    //Components
    [SerializeField] private GameObject Hopps;
    public normalAttack nAttack;
    private Enemy enemy;

    //Variables
    public const string ENEMY_TAG = "Enemy";
    public int damage = 1;
    public bool canHitCooldown = false;

    private void FixedUpdate()
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
        //Debug.Log($"{collision} is in attack range");
        if (nAttack.violent)
        {
            if (collision.gameObject.tag == ENEMY_TAG && !canHitCooldown)
            {
                enemy = collision.gameObject.GetComponent<Enemy>();

                Debug.Log($"{enemy.name} was Hit");
                
                if (enemy.GetComponent<DollStacy>() != null || 
                    enemy.GetComponent<DollLucy>() != null ||
                    enemy.GetComponent<EggEd>() != null ||
                    enemy.GetComponent<FrogGuy>() != null)
                {
                    enemy.TakeDamage(damage, 2);
                }
                else
                {
                    enemy.TakeDamage(damage, 1);
                }

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
